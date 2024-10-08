﻿using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser>> GetAllAsync();
    Task<AppUser?> GetByIdAsync(int id);
    Task<AppUser?> GetByUsernameNameAsync(string usernameName);
    Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams);
    Task<MemberDTO?> GetMemberAsync(string username);
}
