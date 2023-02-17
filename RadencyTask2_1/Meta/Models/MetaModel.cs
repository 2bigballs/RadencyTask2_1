

namespace RadencyTask2_1.Meta.Models
{
    public class MetaModel
    {
        public event Action? ChangeErrorHandler;
        public int ParsedFiles { get; set; } = 1;
        public int ParsedLines { get; set; }

        private int _foundErrors;
        public int FoundErrors
        {
            get => _foundErrors;

            set
            {
                if (value != _foundErrors)
                {
                    ChangeErrorHandler?.Invoke();
                }
                _foundErrors = value;
            }
        }
        public List<string> InvalidFiles { get; set; } = new List<string>();


    }
}
