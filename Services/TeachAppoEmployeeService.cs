using Microsoft.EntityFrameworkCore;
using wuchmiITHome.Data;
using wuchmiITHome.Models;

namespace wuchmiITHome.Services;

/// <summary>
/// Service layer for TeachAppoEmployee business logic.
/// Per constitution Principle II - business logic resides in service classes.
/// </summary>
public class TeachAppoEmployeeService
{
    private readonly wuchmiITHomeContext _context;

    public TeachAppoEmployeeService(wuchmiITHomeContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all teach appo employee records (read-only).
    /// </summary>
    public async Task<List<TeachAppoEmployee>> GetAllAsync()
    {
        return await _context.TeachAppoEmployees
            .AsNoTracking()
            .OrderByDescending(e => e.Yr)
            .ThenBy(e => e.IdNo)
            .ToListAsync();
    }

    /// <summary>
    /// Get a single record by composite key (read-only).
    /// </summary>
    public async Task<TeachAppoEmployee?> GetByIdAsync(int yr, string idNo, DateTime birthday)
    {
        return await _context.TeachAppoEmployees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Yr == yr && e.IdNo == idNo && e.Birthday == birthday);
    }

    /// <summary>
    /// Check if a record with the given composite key exists.
    /// </summary>
    public async Task<bool> ExistsAsync(int yr, string idNo, DateTime birthday)
    {
        return await _context.TeachAppoEmployees
            .AsNoTracking()
            .AnyAsync(e => e.Yr == yr && e.IdNo == idNo && e.Birthday == birthday);
    }

    /// <summary>
    /// Create a new teach appo employee record.
    /// Returns null if duplicate key exists.
    /// </summary>
    public async Task<TeachAppoEmployee?> CreateAsync(TeachAppoEmployee employee)
    {
        // Check for duplicate key
        if (await ExistsAsync(employee.Yr, employee.IdNo, employee.Birthday))
        {
            return null;
        }

        // Timestamps are set by DbContext SaveChanges override
        _context.TeachAppoEmployees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    /// <summary>
    /// Update an existing teach appo employee record.
    /// Primary key fields (Yr, IdNo, Birthday) are immutable.
    /// Returns null if record not found.
    /// </summary>
    public async Task<TeachAppoEmployee?> UpdateAsync(int yr, string idNo, DateTime birthday, TeachAppoEmployee updatedEmployee)
    {
        var existing = await _context.TeachAppoEmployees
            .FirstOrDefaultAsync(e => e.Yr == yr && e.IdNo == idNo && e.Birthday == birthday);

        if (existing == null)
        {
            return null;
        }

        // Update allowed fields only (primary key fields are immutable)
        existing.EmplNo = updatedEmployee.EmplNo;
        existing.ChName = updatedEmployee.ChName;
        existing.EnName = updatedEmployee.EnName;
        existing.Email = updatedEmployee.Email;
        existing.RefreshToken = updatedEmployee.RefreshToken;
        existing.RefreshTokenExpired = updatedEmployee.RefreshTokenExpired;

        // UpdateDate is set by DbContext SaveChanges override
        await _context.SaveChangesAsync();
        return existing;
    }

    /// <summary>
    /// Delete a teach appo employee record.
    /// Returns true if deleted, false if not found.
    /// </summary>
    public async Task<bool> DeleteAsync(int yr, string idNo, DateTime birthday)
    {
        var existing = await _context.TeachAppoEmployees
            .FirstOrDefaultAsync(e => e.Yr == yr && e.IdNo == idNo && e.Birthday == birthday);

        if (existing == null)
        {
            return false;
        }

        _context.TeachAppoEmployees.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
