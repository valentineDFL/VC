namespace VC.Utilities;

public interface IHasTenant
{
    public Guid TenantId { get; set; } // настроить ef фильтрацию таким образом что-бы из базы данных данные приходили уже отфильтрованными
}