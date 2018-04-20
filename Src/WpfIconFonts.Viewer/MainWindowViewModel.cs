using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;
using Algel.WpfTools.Linq;
using Algel.WpfTools.Windows.Data;
using EnumsNET;
using JetBrains.Annotations;

namespace Algel.WpfIconFonts.Viewer
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CollectionView<EnumMember<FontIcon>> _iconSource;
        private double _fontSize;
        private double _width;
        private double _height;
        private string _iconFilter;
        private EnumMember<FlipOrientation> _flip;
        private double _rotation;
        private bool _isSpin;
        private double _spinDuration;
        private Brush _foreground;
        private Brush _background;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc />
        public MainWindowViewModel()
        {
            _iconSource = Enums.GetMembers<FontIcon>().OrderBy(e => e.Name).ToArray().ToCollectionView().SetFilter(FilterIcons);
            _iconSource.CurrentChanged += OnIiconSource_CurrentChanged;

            FlipOrientationSource = Enums.GetMembers<FlipOrientation>().ToArray();
            _flip = FlipOrientationSource.First();

            _fontSize = 50d;
            _width = _height = 50d;
            _spinDuration = 5;

            BrushSource = typeof(Brushes).GetProperties(BindingFlags.Static | BindingFlags.Public).Select(e => new { e.Name, Value = (Brush)e.GetValue(null, null) }).ToArray();
        }

        public string Title => $"FontAwesome v5 Icons viewer ({(_iconSource.SourceCollection as IList)?.Count})";

        private void OnIiconSource_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(FontAwesomeExampleString));
            RaisePropertyChanged(nameof(ImageAwesomeExampleString));
        }

        private bool FilterIcons(EnumMember<FontIcon> item)
        {
            if (string.IsNullOrWhiteSpace(IconFilter) || string.IsNullOrWhiteSpace(item.Name))
                return true;

            return item.Name.ToUpperInvariant().Contains(IconFilter.ToUpperInvariant());
        }

        [NotifyPropertyChangedInvocator]
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            switch (propertyName)
            {
                case nameof(IconFilter):
                    IconSource.Refresh();
                    break;

                case nameof(FontSize):
                    RaisePropertyChanged(nameof(FontAwesomeExampleString));
                    break;

                case nameof(Width):
                case nameof(Height):
                    RaisePropertyChanged(nameof(ImageAwesomeExampleString));
                    break;

                case nameof(Flip):
                case nameof(Rotation):
                case nameof(IsSpin):
                case nameof(SpinDuration):
                    RaisePropertyChanged(nameof(FontAwesomeExampleString));
                    RaisePropertyChanged(nameof(ImageAwesomeExampleString));
                    break;
            }
        }

        public CollectionView<EnumMember<FontIcon>> IconSource
        {
            get => _iconSource;
            private set
            {
                if (Equals(value, _iconSource)) return;
                _iconSource = value;
                RaisePropertyChanged();
            }
        }

        public IList<EnumMember<FlipOrientation>> FlipOrientationSource { get; }

        public EnumMember<FlipOrientation> Flip
        {
            get => _flip;
            set
            {
                if (Equals(value, _flip)) return;
                _flip = value;
                RaisePropertyChanged();
            }
        }

        public string IconFilter
        {
            get => _iconFilter;
            set
            {
                if (value == _iconFilter) return;
                _iconFilter = value;
                RaisePropertyChanged();
            }
        }

        public double FontSize
        {
            get => _fontSize;
            set
            {
                if (value.Equals(_fontSize))
                    return;
                _fontSize = value;
                RaisePropertyChanged();
            }
        }

        public double Width
        {
            get => _width;
            set
            {
                if (value.Equals(_width)) return;
                _width = value;
                RaisePropertyChanged();
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                if (value.Equals(_height)) return;
                _height = value;
                RaisePropertyChanged();
            }
        }

        public double Rotation
        {
            get => _rotation;
            set
            {
                if (value.Equals(_rotation)) return;
                _rotation = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSpin
        {
            get => _isSpin;
            set
            {
                if (value == _isSpin) return;
                _isSpin = value;
                RaisePropertyChanged();
            }
        }

        public double SpinDuration
        {
            get => _spinDuration;
            set
            {
                if (value.Equals(_spinDuration)) return;
                _spinDuration = value;
                RaisePropertyChanged();
            }
        }

        private void AppendCommonProperties(StringBuilder sb)
        {
            if (Flip != null && Flip.Value != FlipOrientation.Normal)
                sb.Append($"FlipOrientation=\"{Flip.Name}\" ");
            if (Rotation > 0)
                sb.Append($"Rotation=\"{Math.Round(Rotation, 0)}\" ");

            if (IsSpin)
            {
                sb.Append("Spin=\"True\" ");

                if (SpinDuration > 0)
                    sb.Append($"SpinDuration=\"{Math.Round(SpinDuration, 1)}\" ");
            }
        }

        public string FontAwesomeExampleString
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append($"<fa:FontAwesome Icon=\"{IconSource.CurrentItem.Name}\" FontSize=\"{Math.Round(FontSize, 1)}\" ");
                AppendCommonProperties(sb);
                sb.Append("/>");
                return sb.ToString();
            }
        }

        public string ImageAwesomeExampleString
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append($"<fa:ImageAwesome Icon=\"{IconSource.CurrentItem.Name}\" Width=\"{Math.Round(Width, 1)}\" Height=\"{Math.Round(Height, 1)}\"");
                AppendCommonProperties(sb);
                sb.Append("/>");
                return sb.ToString();
            }
        }

        public IList BrushSource { get; }

        public Brush Foreground
        {
            get => _foreground ?? Brushes.Black;
            set
            {
                if (Equals(value, _foreground)) return;
                _foreground = value;
                RaisePropertyChanged();
            }
        }

        public Brush Background
        {
            get => _background ?? Brushes.White;
            set
            {
                if (Equals(value, _background)) return;
                _background = value;
                RaisePropertyChanged();
            }
        }
    }
}
