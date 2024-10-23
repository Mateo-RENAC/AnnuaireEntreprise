using AnnuaireEntreprise.Data;
using AnnuaireEntreprise.Models;
using System.Collections.Generic;
using System.Linq;

public class ServicesService : IServicesService
{
    private readonly AnnuaireContext _context;

    public ServicesService(AnnuaireContext context)
    {
        _context = context;
    }

    public List<Service> GetAllServices()
    {
        return _context.Services.ToList();
    }

    public void AddService(Service service)
    {
        _context.Services.Add(service);
        _context.SaveChanges();
    }

    public void UpdateService(Service service)
    {
        _context.Services.Update(service);
        _context.SaveChanges();
    }

    public void DeleteService(Service service)
    {
        _context.Services.Remove(service);
        _context.SaveChanges();
    }
}
