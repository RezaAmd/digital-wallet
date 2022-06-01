using System.Collections.Generic;

namespace Application.Services.WebService.ZarinPal.Model
{
    public class ResultZarinPal<T>
    {
        public T data { get; set; }
        public List<string> errors { get; set; }
    }
}