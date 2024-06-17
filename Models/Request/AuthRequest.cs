
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WSVenta.Models.Request;
public class AuthRequest
{
    [FromHeader]
    [Required]
    public string Email {  get; set; }
    [Required]
    [FromHeader]
    public string Password {  get; set; }   
}
