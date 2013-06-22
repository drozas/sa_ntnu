
/* Movie Billboard program for use in TDT4240
   by Finn Olav Bj�rnson, 2005
*/

import java.awt.*;


/**
 * Concrete billboard display class
 */
class LargeBillboard extends Billboard {

   /**
    * Constructor, creating a new Window, setting the fontsize and moving it so the different
    * billboards don't overlap
    */
   public LargeBillboard()
   {
      output = new Window("Large Billboard",408,88);

      output.setFont("Arial", 12);

      output.moveWindow(0, 100);

      output.repaint();
      
   }

   /**
    * Method responsible for putting new text in the window and scrolling it to make room for
    * the next text
    *
    * @param Movie The movie which is to be displayed in the window
    */
   public void showNextMovie(Movie movie)
   {
      // determine presentation size of new movie info:
      int nameWidth;     // pixels needed for name
      int theaterWidth;  // pixels needed for price
      int timeWidth;     // pixels needed for volume
      int tempMaxWidth;  // maximum of theaterWidth and nameWidth
      int maxWidth;      // maximum of nameWidth, theaterWidth and timeWidth

      int x;             // temporary variable used to hold the x position of a text
      int y;             // temporary variable used to hold the y position of a text

      // Determining the pixelsize of the name, the time and location
      output.setBold(true);
      nameWidth = output.fontWidth( movie.getName() );
      output.setBold(false);
      theaterWidth  = output.fontWidth( movie.getTheater() );
      timeWidth = output.fontWidth( movie.getTime() );
      tempMaxWidth = theaterWidth > nameWidth ? theaterWidth : nameWidth;
      maxWidth = tempMaxWidth > timeWidth ? tempMaxWidth : timeWidth;

      // Drawing the movie name just right of the window
      y = ( (output.getHeight() - output.getFontMetrics(output.getFont()).getHeight() ) / 2 ) + output.getFont().getSize();
      x = output.getWidth();
      output.setBold(true);
      output.drawString(movie.getName(), x, y);
      output.setBold(false);

      // Drawing the theater information just above the name of the movie
      y = ( (output.getHeight() - output.getFontMetrics(output.getFont()).getHeight() ) / 2 );
      output.drawString(movie.getTheater(), x, y);

      // Drawing the time information just below the name of the movie
      y = ( (output.getHeight() - output.getFontMetrics(output.getFont()).getHeight() ) / 2 ) + (output.getFont().getSize())*2;
      output.drawString(movie.getTime(), x, y);

      // Scrolling the text into the window
      output.moveText(maxWidth + 15);
   }
};