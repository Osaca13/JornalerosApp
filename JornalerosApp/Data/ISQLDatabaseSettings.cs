namespace JornalerosApp.Data
{
    public interface ISQLDatabaseSettings
    {
        string DataSource { get; set; }
        string InitialCatalog { get; set; }
        string Password { get; set; }
        bool PersistsecurityInfo { get; set; }
        string UserId { get; set; }
    }
}