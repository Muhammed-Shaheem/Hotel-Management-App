using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HotelAppWebUI.Pages;

public class BookRoomModel : PageModel
{

    private readonly IDatabaseData db;

    [BindProperty(SupportsGet = true)]
    public int RoomTypeId { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime StartDate { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateTime EndDate { get; set; }

    [Required]
    [BindProperty]
    public string FirstName { get; set; }

    [Required]
    [BindProperty]
    public string LastName { get; set; }
    public RoomTypeModel? RoomType { get; private set; }

    public BookRoomModel(IDatabaseData db)
    {
        this.db = db;
    }

    public void OnGet()
    {
        RoomType = db.GetRoomTypeById(RoomTypeId);


    }

    public IActionResult OnPost()
    {
        db.BookGuest(FirstName, LastName, StartDate, EndDate, RoomTypeId);
        return Redirect("/Index");
    }


}
