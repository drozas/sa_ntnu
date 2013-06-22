
/* Movie Billboard program for use in TDT4240
   by Finn Olav Bj�rnson, 2005
*/

import java.lang.InterruptedException;


/**
 * Main class: fetches data and updates the displays
 */
class MovieInfoSource
{
   private Movie       lastMovie;
   private MovieStream source;
   private Billboard   simple;
   
   //1a) create a private LargeBillBoard attribute
   private LargeBillboard large;
   
   //1b) create a private NovaBillBoard attribute
   private NovaBillboard nova;

   /**
    * Constructor, initialises a moviestream, a simple billboard and then hands
    * control over to showMovieInfo method
    *
    * @param fileName Name of the file that contains movie information
    */
   public MovieInfoSource(String fileName)
   {
	   boolean anyOpen = false;
      source = new FakeMovieStream(fileName);
      simple = new SimpleBillboard();
      anyOpen = simple.isVisible();
      
      // 1a) instantiate LargeBillBoard 
      large = new LargeBillboard();
      anyOpen = large.isVisible() || anyOpen;

      // 1a) instantiate NovaBillBoard 
      nova = new NovaBillboard();
      anyOpen = nova.isVisible() || anyOpen;
      
      //1c) exit if all the billboards were cancelled
      if (anyOpen)
    	  showMovieInfo();
      else
    	  System.exit(0);
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
         lastMovie = source.getMovie();
         simple.showNextMovie(lastMovie);
         
         //1a) show largeBillboard with the lastMovie
         large.showNextMovie(lastMovie);
         
         //1b) show novaBillboard with the lastMovie
         nova.showNextMovie(lastMovie);
      }
    }

};