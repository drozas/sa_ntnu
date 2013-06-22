/* Movie Billboard program for use in TDT4240
   by Finn Olav Bj�rnson, 2005
*/


import java.io.BufferedReader;
import java.io.FileReader;
import java.lang.StringBuffer;
import java.io.IOException;

/**
 *  class implementing a source of simulated movie events
 **/
class FakeMovieStream implements MovieStream{

   private BufferedReader textBuffer;

   /**
    * Method handles reading from file and putting the result in a BufferedReader object
    *
    * @param String Name of the file we want to read from
    */
   FakeMovieStream(String fileName)
   {
      try
      {
         FileReader inFile = new FileReader(fileName);
         textBuffer = new BufferedReader(inFile);
         textBuffer.mark(600);
      }
      catch(IOException i)
      {
         System.out.println("Error opening data file!");
      }
   }

   /**
    * Method returns the next movie to be displayed.
    * NOTE: This is just a simple simulation of a real stream of movie
    * information. It reads records from a file (and starts
    * over when the file is exhausted). In reality this should contact
    * some online information service to receive its information.
    *
    * @return Movie Object which contains information about the last movie
    */
   public Movie getMovie()
   {
      String lineBuffer;
      StringBuffer line;
      String name    = null;
      String time    = null;
      String theater = null;
      int start;
      int stop;

      // Read a new line from the text buffer
      try
      {
         lineBuffer = textBuffer.readLine();

         // if file was read to end, start again
         if(lineBuffer == null)
         {
            textBuffer.reset();
            lineBuffer = textBuffer.readLine();
         }

         // Converting String to StringBuffer to extract items
         line = new StringBuffer(lineBuffer);

         // Getting the name of the movie
         start = 0;
         stop  = line.indexOf(";", start);
         name  = line.substring(start, stop);

         // Getting the theater the movie shows in
         start   = stop+1;
         stop    = line.indexOf(";", start);
         theater = line.substring(start, stop);

         // Getting the time the movie runs
         start = stop+1;
         stop  = line.length();
         time  = line.substring(start, stop);
      }
      catch(IOException i)
      {
         System.out.println("Error reading data file!");
      }

      // Creating new Movie object and returning it from the method
      return new Movie(name, time, theater);
   }
};