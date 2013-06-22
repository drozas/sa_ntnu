/* Movie Billboard program for use in TDT4240
   by Finn Olav Bj�rnson, 2005
*/

import java.util.Observable;

/**
 * Main class: fetches data and updates the displays
 */
class MovieInfoSource extends Observable
{
   private Movie       lastMovie;
   private MovieStream source;

   /**
    * Constructor, initialises a moviestream, a simple billboard and then hands
    * control over to showMovieInfo method
    *
    * @param fileName Name of the file that contains movie information
    */
   public MovieInfoSource(String fileName)
   {
      source = new FakeMovieStream(fileName);
   }

   /**
    * Method contains an infinite loop that gets a new Movie object and then instructs a
    * window to show this information
    */
   public void showMovieInfo()
    {
      boolean done = false;
      while( !done )
      {
    	  //Changes in lastMovie are observed and notified
         lastMovie = source.getMovie();
         setChanged();
         notifyObservers(lastMovie);
      }
    }

};