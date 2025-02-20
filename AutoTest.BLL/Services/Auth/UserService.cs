using AutoMapper;
using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.Common.Security;
using AutoTest.BLL.DTOs.User;
using AutoTest.BLL.Interfaces.Auth;
using AutoTest.DAL.Interfaces;
using AutoTest.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AutoTest.BLL.Services.Auth;

public class UserService(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ITokenService service) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ITokenService _service = service;
    public async Task<bool> ChangePasswordAsync(long id, UserChangePasswordDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var user = await _unitOfWork.User.GetById(id);
            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            if (!PasswordHelper.Verify(dto.CurrentPassword, user.Password, user.Salt))
                return false;

            var hasher = PasswordHelper.Hash(dto.NewPassword);
            user.Password = hasher.Hash;
            user.Salt = hasher.Salt;

            var result = await _unitOfWork.User.Update(user);
            return result;
        }
        catch (Exception ex)
        {
            throw new StatusCodeException(HttpStatusCode.InternalServerError, $"An error occured while changing password. {ex.Message}");
        }
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellation = default)
    {
        try
        {
            var user = await _unitOfWork.User.GetById(id);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            var result = await _unitOfWork.User.Delete(user);
            return result;
        }
        catch (Exception ex)
        {
            throw new StatusCodeException(HttpStatusCode.InternalServerError, $"An error occured while deleting a user. {ex.Message}");
        }
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellation = default)
    {
        try
        {
            var users = await _unitOfWork.User.GetAll().ToListAsync(cancellation);

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserDto> GetByIdAsync(long id, CancellationToken cancellation = default)
    {
        try
        {
            var user = await _unitOfWork.User.GetById(id);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            return _mapper.Map<UserDto>(user);
        }
        catch (Exception ex)
        {
            throw new StatusCodeException(HttpStatusCode.InternalServerError, $"An errror occured while getting a user by id. {ex.Message}");
        }
    }

    public async Task<string> LoginAsync(LoginDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var user = await _unitOfWork.User.GetAll().FirstOrDefaultAsync(x => x.PhoneNumber == dto.PhoneNumber);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Phone number or password wrong!");

            var hasherResult = PasswordHelper.Verify(dto.Password, user.Password, user.Salt);
            if (!hasherResult)
                throw new StatusCodeException(HttpStatusCode.BadRequest, "Password wrong!");

            return _service.GenerateToken(user);
        }
        catch (Exception ex)
        {
            throw new StatusCodeException(HttpStatusCode.InternalServerError, $"An error occured while logging in. {ex.Message}");
        }
    }

    public async Task<bool> RegisterAsync(RegisterDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var user = await _unitOfWork.User.GetAll().FirstOrDefaultAsync(x => x.PhoneNumber == dto.PhoneNumber);
            if (user is not null)
                throw new StatusCodeException(HttpStatusCode.BadRequest, "User already exists!");

            var hasher = PasswordHelper.Hash(dto.Password);
            var mapped = _mapper.Map<User>(dto);

            mapped.CreatedDate = DateTime.UtcNow.AddHours(5);
            mapped.Salt = hasher.Salt;
            mapped.Password = hasher.Hash;

            var result = await _unitOfWork.User.Add(mapped);

            return result;
        }
        catch (Exception ex)
        {
            throw new StatusCodeException(HttpStatusCode.InternalServerError, $"An error occured while registering a user. {ex.Message}");
        }
    }

    public async Task<bool> UpdateAsync(long id, UpdateUserDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var user = await _unitOfWork.User.GetById(id);
            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            var mapped = _mapper.Map(dto, user);
            mapped.Id = id;
            mapped.UpdatedDate = DateTime.UtcNow.AddHours(5);

            var result = await _unitOfWork.User.Update(mapped);
            return result;
        }
        catch (Exception ex)
        {
            throw new StatusCodeException(HttpStatusCode.InternalServerError, $"An error occured while updating a user. {ex.Message}");
        }
    }

    public async Task<bool> VerifyPasswordAsync(long id, string password, CancellationToken cancellation = default)
    {
        try
        {
            var user = await _unitOfWork.User.GetById(id);
            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            if (!PasswordHelper.Verify(password, user.Password, user.Salt))
                return false;

            return true;
        }
        catch (Exception ex)
        {
            throw new StatusCodeException(HttpStatusCode.InternalServerError, $"An error occured while verifying a password. {ex.Message}");
        }
    }
}
