using HyzenMeme.Infrastructure;
using HyzenMeme.Models;
using Microsoft.AspNetCore.Connections;
using ConnectionContext = HyzenMeme.Infrastructure.ConnectionContext;

namespace HyzenMeme.Repositories;

public class ImagesRepository
{
    private readonly ConnectionContext _context = new();

    public void Add(Images image)
    {
        _context.Images.Add(image);
        _context.SaveChanges();
    }
}