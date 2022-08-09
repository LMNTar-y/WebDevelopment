using java.sql;
using WebDevelopment.Domain;

namespace WebDevelopment.API
{
    public class ResponseWrapper<TModel> where TModel : class
    {
        public TModel? Result { get; set; }

        public ICollection<Error> Errors { get; set; }

        public ResponseWrapper()
        {
            // Prevent nulls in the response
            Errors = new List<Error>();
        }
    }
}
