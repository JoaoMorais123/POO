namespace WpfApp1;

public class SingletonToken
{
    private static SingletonToken instance;

    private SingletonToken() { }

    public static SingletonToken Instance
    {
        get
        {
            if (instance == null)
                instance = new SingletonToken();
            return instance;
        }
    }
    
    public string Token { get; set; }
}