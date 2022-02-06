namespace Application.Models.Dto
{
    public class PageParam
    {
        private int _number { get; set; }
        public int? page
        {
            get
            {
                if (_number < 1)
                    return 1;
                return _number;
            }
            set
            {
                if (value.HasValue)
                    if (value.Value > 0)
                    {
                        _number = value.Value;
                        return;
                    }
                _number = 1;
            }
        }

        private int? _size { get; set; }
        public int? pageSize
        {
            get { return _size; }
            set
            {
                if (value.HasValue)
                    if (value.Value < 1)
                    {
                        _size = 1;
                        return;
                    }
                _size = value.Value;
            }
        }
    }
}