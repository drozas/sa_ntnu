import java.util.Observable;
import java.util.Observer;

/* Movie Billboard program for use in TDT4240
   by Finn Olav Bj�rnson, 2005

   class list and inheritance hierarchy:

   class Movie

   interface MovieStream
      class FakeMovieStream

   class Billboard
      class SimpleBillboard
      class LargeBillboard

   class MovieInfoSource
*/

/**
 * Interface for all billboard display classes, also contains main method which starts the program
 * and passes control to the main class MovieInfoSource
 */
public abstract class Billboard implements Observer
{
   protected Window output;
   protected boolean isActive;
   
   public boolean isActive()
   {
	   return isActive;
   }

   public boolean isVisible(){
	   boolean answer = output.askYesNo("Do you want to show all contents in a " + output.getTitle()+"?");
	   if (!answer)
	      {
	    	  output.dispose();
	    	  isActive = false;
	      }
	      else
	      {
	    	  
	    	  output.setVisible(true);
	    	  isActive = true;
	      }
	   
	   return answer;
   };
   
   public abstract void showNextMovie(Movie movie);

   /**
    * Main method, starts a billboard using data from Trondheim Kino in file data.txt
    */
   public static void main(String[] args)
   {
	   boolean anyOpen = false;
      MovieInfoSource mis = new MovieInfoSource( "data_utf8.txt" );
      
      //Instanciate the objects
      SimpleBillboard simple = new SimpleBillboard();
      anyOpen = simple.isVisible();
      
      
      LargeBillboard large = new LargeBillboard();
      anyOpen = large.isVisible() || anyOpen;
      
      NovaBillboard nova = new NovaBillboard();
      anyOpen = nova.isVisible() || anyOpen;
      
     
      //exit if all the billboards were cancelled
      if (anyOpen)
      {
          //Add them as observers
          if (simple.isActive())
        	  mis.addObserver(simple);
          if (large.isActive())
        	  mis.addObserver(large);
          if (nova.isActive())
        	  mis.addObserver(nova);
          
          //Read movies from source
          mis.showMovieInfo();
      }else{
    	  System.exit(0);
      }

   }
   
   public void update(Observable obj, Object arg)
   {	   
	   Movie movie;

	   if (arg instanceof Movie)
	   {
		   movie = (Movie)arg;
		   
		   //If a new movie has been notified, show it
		   this.showNextMovie(movie);
	   }
   
   }

};