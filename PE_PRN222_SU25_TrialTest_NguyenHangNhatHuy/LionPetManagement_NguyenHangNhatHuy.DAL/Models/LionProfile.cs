using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LionPetManagement_NguyenHangNhatHuy.DAL.Models;

public partial class LionProfile
{
	public int LionProfileId { get; set; }

	// [Required(ErrorMessage = "Lion Type is required")]
	public int LionTypeId { get; set; }

	[Required(ErrorMessage = "Lion Name is required")]
	[StringLength(100, ErrorMessage = "Lion Name cannot be longer than 100 characters")]
	public string LionName { get; set; } = null!;

	[Required(ErrorMessage = "Weight is required")]
	[Range(0.1, 1000, ErrorMessage = "Weight must be between 0.1 and 1000 kg")]
	public double Weight { get; set; }

	[Required(ErrorMessage = "Characteristics is required")]
	[StringLength(500, ErrorMessage = "Characteristics cannot be longer than 500 characters")]
	public string Characteristics { get; set; } = null!;

	[Required(ErrorMessage = "Warning is required")]
	[StringLength(500, ErrorMessage = "Warning cannot be longer than 500 characters")]
	public string Warning { get; set; } = null!;

	public DateTime ModifiedDate { get; set; }

	public virtual LionType? LionType { get; set; } = null!;
}
