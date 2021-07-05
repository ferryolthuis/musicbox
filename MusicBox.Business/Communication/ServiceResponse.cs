namespace MusicBox.Business.Communication
{
    public class ServiceResponse<T> where T : class
    {
        private ServiceResponse(bool success, string message, T item)
        {
            Success = success;
            Message = message;
            Item = item;
        }

        internal ServiceResponse(T item) : this(true, string.Empty, item)
        {
        }

        internal ServiceResponse(string errorMessage) : this(false, errorMessage, null)
        {
        }

        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        public T Item { get; set; }
    }
}
