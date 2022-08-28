namespace GodBot.Core.Interfaces
{
    /// <summary>
    /// An interface for humor related operations.
    /// </summary>
    public interface IHumorService
    {
        /// <summary>
        /// Get's a random joke.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// A <see langword="string"/>, containing the joke.
        /// </returns>
        public Task<string> GetRandomJokeAsync();

        /// <summary>
        /// Get's a random meme.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation.
        /// A <see langword="string"/>, containing the meme.
        /// </returns>
        public Task<string> GetRandomMemeAsync();
    }
}