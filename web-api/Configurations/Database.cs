namespace web_api.Configurations
{
    public class Database
    {
        //PC CASA
        //<add name="consultorio" connectionString="Server=DESKTOP-ORAHIVS\SQLEXPRESS;Database=consultorio;Trusted_Connection=True;"/>

        //PC LOJA
        //<add name="consultorio" connectionString="Server=DESKTOP-H5DDF2K\SQLEXPRESS;Database=consultorio;Trusted_Connection=True;"/>

        public static string getConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["consultorio"].ConnectionString;
        }
    }
}