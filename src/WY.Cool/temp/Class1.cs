namespace temp
{
    public class Class1
    {
        public string GetMyType<T>() where T : class
        {
            return GetMyType(typeof(T));
        }

        public string GetMyType(Type type)
        {
            return type.Name;
        }
        
        public void Run()
        {
            // 执行上述方法
            this.GetMyType<object>();
            //或者
            this.GetMyType(typeof(string));




        }
    }
}