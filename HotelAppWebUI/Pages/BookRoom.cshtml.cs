using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HotelAppWebUI.Pages;

public class BookRoomModel : PageModel
{
    private int roomTypeId;

    [BindProperty]
    [Required]
    
    public string FirstName { get; set; }
    [BindProperty]
    [Required]
    public string LastName { get; set; }
    

    public void OnGet(int Id)
    {
        roomTypeId = Id;
    }


}
