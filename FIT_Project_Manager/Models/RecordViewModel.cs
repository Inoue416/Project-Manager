using System.ComponentModel.DataAnnotations;

namespace FIT_Project_Manager.Models;

public class RecordViewModel
{
    [Display(Name="タイトル")]
    [Required(ErrorMessage = "タイトルを入力してください。")]
    [StringLength(1000)]
    public string? Title { get; set; }

    [Display(Name="内容")]
    [Required(ErrorMessage = "内容を入力してください。")]
    [StringLength(1000000)]
    public string? Content { get; set; }
}