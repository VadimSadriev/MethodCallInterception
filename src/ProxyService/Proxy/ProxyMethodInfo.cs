namespace ProxyService.Proxy
{
    public class ProxyMethodInfo
    {
        public ProxyMethodInfo(string name, object[] args)
        {
            Name = name;
            Args = args;
        }

        public string Name { get; }
        public object[] Args { get; }
    }
}
