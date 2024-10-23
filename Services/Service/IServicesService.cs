using AnnuaireEntreprise.Models;

public interface IServicesService
{
    List<Service> GetAllServices();
    void AddService(Service service);
    void UpdateService(Service service);
    void DeleteService(Service service);
}
