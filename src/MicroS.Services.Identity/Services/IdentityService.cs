﻿using System;
using System.Threading.Tasks;
using MicroS.Services.Identity.Domain;
using MicroS.Services.Identity.Messsages.Events;
using MicroS.Services.Identity.Repositories;
using MicroS_Common.Authentication;
using MicroS_Common.RabbitMq;
using MicroS_Common.Types;
using Microsoft.AspNetCore.Identity;

namespace MicroS.Services.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IClaimsProvider _claimsProvider;
        private readonly IBusPublisher _busPublisher;

        public IdentityService(IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtHandler jwtHandler,
            IRefreshTokenRepository refreshTokenRepository,
            IClaimsProvider claimsProvider,
            IBusPublisher busPublisher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
            _refreshTokenRepository = refreshTokenRepository;
            _claimsProvider = claimsProvider;
            _busPublisher = busPublisher;
        }

        public async Task SignUpAsync(Guid id, string email, string password, string role = Role.User)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new MicroSException(Codes.EmailInUse,
                    $"Email: '{email}' is already in use.");
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                role = Role.User;
            }
            user = new User(id, email, role);
            user.SetPassword(password, _passwordHasher);
            await _userRepository.AddAsync(user);
            await _busPublisher.PublishAsync(new SignedUp(id, email, role), CorrelationContext.Empty);
        }

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null || !user.ValidatePassword(password, _passwordHasher))
            {
                throw new MicroSException(Codes.InvalidCredentials,
                    "Invalid credentials.");
            }
            var refreshToken = new RefreshToken(user, _passwordHasher);
            var claims = await _claimsProvider.GetAsync(user.Id);
            var jwt = _jwtHandler.CreateToken(user.Id.ToString("N"), user.Role, claims);
            jwt.RefreshToken = refreshToken.Token;
            await _refreshTokenRepository.AddAsync(refreshToken);

            return jwt;
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new MicroSException(Codes.UserNotFound,
                    $"User with id: '{userId}' was not found.");
            }
            if (!user.ValidatePassword(currentPassword, _passwordHasher))
            {
                throw new MicroSException(Codes.InvalidCurrentPassword,
                    "Invalid current password.");
            }
            user.SetPassword(newPassword, _passwordHasher);
            await _userRepository.UpdateAsync(user);
            await _busPublisher.PublishAsync(new PasswordChanged(userId), CorrelationContext.Empty);
        }

        Task<JsonWebToken> IIdentityService.SignInAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}