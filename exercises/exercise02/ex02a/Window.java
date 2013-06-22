
/* Movie Billboard program for use in TDT4240
   by Finn Olav Bj�rnson, 2005
*/

import java.awt.*;
import java.util.ArrayList;
import javax.swing.*;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;

/**
 * Windows class, implementing a simple window that handles all the graphics, you don't need
 * to change anything here. (Unless you really, really, really want to!)
 */
public class Window extends JPanel
{
   private ArrayList content;
   private JFrame frame;

   /**
    * Constructor, creates a new window
    *
    * @param windowName, the name to be displayed as a windowname
    * @param xlength, the length of the window
    * @param ylength, the height of the window
    */
   public Window(String windowName, int xlength, int ylength)
   {
      frame = new JFrame(windowName);
     
      Dimension d = Toolkit.getDefaultToolkit().getScreenSize();
      frame.setBounds( (d.width - xlength) / 2, (d.height - ylength) / 2, xlength, ylength);
      frame.setBackground(Color.white);
      frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

      content = new ArrayList();

      this.setBackground(Color.white);
      this.setSize(new Dimension(xlength,ylength));

      frame.getContentPane().add(this);
      frame.setVisible(true);
   }

   /**
    * Method for moving the window, it is initially placed at the center of the screen
    *
    * @param xdir, how much the window is to be moved in the x direction (- is left, + is right)
    * @param ydir, how much the window is to be moved in the y direction (- is up, + is down)
    */
   public void moveWindow(int xdir, int ydir)
   {
      frame.setBounds(frame.getX()+xdir, frame.getY()+ydir, frame.getWidth(), frame.getHeight());
   }

   /**
    * Method for setting text in the window
    *
    * @param contents, the string to be drawn
    * @param xpos, horizontal position for the upper left corner of the text
    * @param ypos, vertical position for the upper left corner of the text
    */
   public void drawString(String contents, int xpos, int ypos)
   {
      Text text = new Text(contents, getFont(), xpos, ypos );
      content.add(text);
   }

   /**
    * Method for setting the font for text in the window
    *
    * @param font, fontname
    * @param size, fontsize
    */
   public void setFont(String font, int size)
   {
      setFont( new Font(font, -1, size) );
   }

   /**
    * Method returns the width of a given text with the windows current font
    *
    * @param text, the text you want to know the width of
    * @return int, number of pixels for the given text
    */
   public int fontWidth(String text)
   {
      return getFontMetrics(getFont()).stringWidth(text);
   }

   /**
    * Method for setting the font in the window to bold
    *
    * @param bold, true sets font to bold, false cancels bold font
    */
   public void setBold(boolean bold)
   {
      if(bold)
      {
         setFont(new Font(getFont().getFontName(), 1, getFont().getSize() ));
      }
      else
      {
         setFont(new Font(getFont().getFontName(), 0, getFont().getSize() ));
      }
   }

   /**
    * Method for scrolling the text in the window
    *
    * @param distance, number of pixels to scroll
    */
   public synchronized void moveText(int distance)
   {
      for(int i = 0; i < distance; i++)
      {
         try
         {
            wait( 20 );
         }
         catch(InterruptedException e)
         {
            System.out.println("Error in artificial delay mechanism");
         }

         if(content.size() > 0)
         {
            Text text = null;

            for(int j = 0; j < content.size(); j++)
            {
               text = (Text)content.get(j);
               text.setX(text.getX() - 1);
               if( text.getX() < 0 - fontWidth(text.getText()) )
               {
                  content.remove(j);
               }
            }
            repaint();
         }
      }
   }

   /**
    * Method for creating a popup window, the window formulates a question specified in the prompt
    * parameter and displays a yes and a no button. The method returns true if yes is pressed and
    * false if no is pressed.
    *
    * @param prompt, the question we want to display
    * @return boolean, true if yes is pressed, false if no is pressed
    */
   public boolean askYesNo(String prompt)
   {
      frame.setVisible(false);
      boolean result = false;
      boolean finished = false;
      setFont("Arial", 12);
      int framewidth;

      if(fontWidth(prompt) > 120 )
      {
         framewidth = (int)( fontWidth(prompt) * 1.25);
      }
      else
      {
         framewidth = 150;
      }

      JFrame contents = new JFrame("Popup");
      Dimension d = Toolkit.getDefaultToolkit().getScreenSize();
      contents.setBounds( (d.width - 200) / 2, (d.height - 70) / 2, framewidth, 90);
      contents.setBackground(Color.white);
      contents.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

      yesListener yes = new yesListener();
      noListener  no  = new noListener();

      PopupFactory factory = new PopupFactory();

      JLabel question = new JLabel(prompt);
      question.setFont( new Font("Arial", 1, 12) );

      JButton yesButton = new JButton("Yes");
      yesButton.setFont( new Font("Arial", 1, 12) );
         yesButton.addActionListener( yes );

         JButton noButton = new JButton("No");
      noButton.setFont( new Font("Arial", 1, 12) );
         noButton.addActionListener( no );

      JPanel main = new JPanel();
      main.setBackground(Color.white);
      main.setLayout( new BoxLayout(main, BoxLayout.Y_AXIS) );
      main.setAlignmentX(Component.CENTER_ALIGNMENT);
      main.setAlignmentY(Component.CENTER_ALIGNMENT);

      JPanel header = new JPanel();
      header.setBackground(Color.white);
      header.setAlignmentX(Component.CENTER_ALIGNMENT);
      header.setAlignmentY(Component.CENTER_ALIGNMENT);
      header.add(question);
      main.add(header);

      contents.getContentPane().add(main, BorderLayout.NORTH);

      JPanel input = new JPanel();
      input.setBackground(Color.white);
      input.setAlignmentX(Component.CENTER_ALIGNMENT);
      input.setAlignmentY(Component.CENTER_ALIGNMENT);

      input.add(yesButton);
      input.add(Box.createRigidArea(new Dimension(10, 0)));
      input.add(noButton);

      contents.getContentPane().add(input, BorderLayout.CENTER);

      contents.setResizable(false);
      contents.setVisible(true);

      while(!finished)
      {
         if(yes.isPressed())
         {
            result = true;
            finished = true;
         }
         if(no.isPressed())
         {
            result = false;
            finished = true;
         }
      }

      contents.setVisible(false);
      return result;
   }

   private class yesListener implements ActionListener
    {
      private boolean buttonPressed = false;

      public void actionPerformed(ActionEvent event)
         {
         buttonPressed = true;
         }

         public boolean isPressed()
         {
         return buttonPressed;
      }
      }

   private class noListener implements ActionListener
    {
      private boolean buttonPressed = false;

      public void actionPerformed(ActionEvent event)
         {
         buttonPressed = true;
         }

         public boolean isPressed()
         {
         return buttonPressed;
      }
      }

   public void paint(java.awt.Graphics g)
   {
      g.setFont(getFont());

      g.clearRect( 0, 0, this.getWidth(), this.getHeight() );

      if(content.size() > 0)
      {
         for(int i = 0; i < content.size(); i++)
         {
            Text text = (Text)content.get(i);
            g.setFont(text.getFont());
            g.drawString(text.getText(), text.getX(), text.getY());
         }
      }
   }
   

   //ex1c) added to have access to close the window
   public void dispose()
   {
	   this.frame.dispose();
   }
   public String getTitle(){
	 return this.frame.getTitle();  
   };
   

   //ex1c) added to have access to make the window visible
   public void setVisible(boolean visible)
   {
	   this.frame.setVisible(visible);
   }
}

class Text
{
   private String text;
   private Font font;
   private int x;
   private int y;

   public Text(String text, Font font)
   {
      this.text = text;
      this.font = font;
   }

   public Text(String text, Font font, int x, int y)
   {
      this(text, font);
      this.x=x;
      this.y=y;
   }

   public String getText()
   {
      return text;
   }

   public Font getFont()
   {
      return font;
   }

   public int getX()
   {
      return x;
   }

   public int getY()
   {
      return y;
   }

   public void setX(int x)
   {
      this.x=x;
   }

   public void setY(int y)
   {
      this.y=y;
   }
}