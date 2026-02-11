using System.ComponentModel.DataAnnotations;

namespace wuchmiITHome.Models;

/// <summary>
/// Represents a single teaching appointment employee record.
/// Primary key is composite: Yr, IdNo, Birthday.
/// </summary>
public class TeachAppoEmployee
{
    /// <summary>
    /// Academic year (part of composite primary key).
    /// </summary>
    [Required]
    public int Yr { get; set; }

    /// <summary>
    /// Identity number (part of composite primary key).
    /// </summary>
    [Required]
    [StringLength(60)]
    public string IdNo { get; set; } = string.Empty;

    /// <summary>
    /// Birth date (part of composite primary key).
    /// </summary>
    [Required]
    [DataType(DataType.Date)]
    public DateTime Birthday { get; set; }

    /// <summary>
    /// Employee number (6 characters).
    /// </summary>
    [Required]
    [StringLength(6, MinimumLength = 6)]
    public string EmplNo { get; set; } = string.Empty;

    /// <summary>
    /// Chinese name of the employee.
    /// </summary>
    [Required]
    [StringLength(60)]
    [Display(Name = "中文姓名")]
    public string ChName { get; set; } = string.Empty;

    /// <summary>
    /// English name of the employee.
    /// </summary>
    [Required]
    [StringLength(60)]
    [Display(Name = "英文姓名")]
    public string EnName { get; set; } = string.Empty;

    /// <summary>
    /// Email address of the employee.
    /// </summary>
    [Required]
    [StringLength(60)]
    [EmailAddress]
    [Display(Name = "電子郵件")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Refresh token value for external API authentication.
    /// </summary>
    [Required]
    [StringLength(60)]
    [Display(Name = "刷新令牌")]
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Refresh token expiration timestamp (optional).
    /// </summary>
    [DataType(DataType.DateTime)]
    [Display(Name = "令牌過期時間")]
    public DateTime? RefreshTokenExpired { get; set; }

    /// <summary>
    /// Database identity sequence number (system-managed, read-only).
    /// </summary>
    public int SeqNo { get; set; }

    /// <summary>
    /// Record creation timestamp (system-managed, read-only).
    /// </summary>
    [DataType(DataType.DateTime)]
    [Display(Name = "建立時間")]
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// Record last update timestamp (system-managed, read-only).
    /// </summary>
    [DataType(DataType.DateTime)]
    [Display(Name = "更新時間")]
    public DateTime UpdateDate { get; set; }
}
