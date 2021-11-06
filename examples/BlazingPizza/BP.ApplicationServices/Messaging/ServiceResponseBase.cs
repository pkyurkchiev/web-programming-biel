namespace BP.ApplicationServices.Messaging
{
    public abstract class ServiceResponseBase
    {
        public ServiceResponseBase()
        {
            this.StatusCode = System.Net.HttpStatusCode.OK;
            this.StatusDesciption = "OK";
        }

        public System.Net.HttpStatusCode StatusCode { get; set; }
        public string StatusDesciption { get; set; }
    }
}
