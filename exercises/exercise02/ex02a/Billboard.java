
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
public abstract class Billboard
{
   protected Window output;

   public abstract void showNextMovie(Movie movie);
   
   public boolean isVisible(){
	   boolean answer = output.askYesNo("Do you want to show all contents in a " + output.getTitle()+"?");
	   if (!answer)
	      {
	    	  output.dispose();
	      }
	      else
	      {
	    	  
	    	  output.setVisible(true);
	      }
	   
	   return answer;
   };

   /**
    * Main method, starts a billboard using data from Trondheim Kino in file data.txt
    */
   public static void main(String[] args)
   {
	   //drozas: change the source to one converted in utf-8
      MovieInfoSource mis = new MovieInfoSource( "data_utf8.txt" );
   }

};