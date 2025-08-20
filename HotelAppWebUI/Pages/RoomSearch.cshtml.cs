using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HotelAppWebUI.Pages;


public class RoomSearchModel : PageModel
{
    private readonly IDatabaseData db;
    public List<RoomTypeModel> roomTypes = new();

    [BindProperty]
    [Required]
    [DataType(DataType.Date)]

    public DateTime StartDate { get; set; } = DateTime.Today;
    [BindProperty]
    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
    public RoomSearchModel(IDatabaseData db)
    {
        this.db = db;
    }
    public void OnGet()
    {

    }

    public IActionResult OnPost()
    {
        roomTypes = db.GetAvailableRoomTypes(StartDate, EndDate);
        return Page();

    }


}
