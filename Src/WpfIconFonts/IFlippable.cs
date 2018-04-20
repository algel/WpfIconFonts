using System.Windows;

namespace Algel.WpfIconFonts
{
    /// <summary>
    /// Defines the different flip orientations that a icon can have.
    /// </summary>
    [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
    public enum FlipOrientation
    {
        /// <summary>
        /// Default
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Flip horizontally (on x-achsis)
        /// </summary>
        Horizontal,
        /// <summary>
        /// Flip vertically (on y-achsis)
        /// </summary>
        Vertical,
    }

    /// <summary>
    /// Represents a flippable control
    /// </summary>
    public interface IFlippable
    {
        /// <summary>
        /// Gets or sets the current orientation (horizontal, vertical).
        /// </summary>
        FlipOrientation FlipOrientation { get; set; }
    }

    /// <summary>
    /// Represents a rotatable control
    /// </summary>
    public interface IRotatable
    {
        /// <summary>
        /// Gets or sets the current rotation (angle).
        /// </summary>
        double Rotation { get; set; }
    }

    /// <summary>
    /// Represents a spinable control
    /// </summary>
    public interface ISpinable
    {
        /// <summary>
        /// Gets or sets the current spin (angle) animation of the icon.
        /// </summary>
        bool Spin { get; set; }

        /// <summary>
        /// Gets or sets the duration of the spinning animation (in seconds). This will stop and start the spin animation.
        /// </summary>
        double SpinDuration { get; set; }
    }
}
