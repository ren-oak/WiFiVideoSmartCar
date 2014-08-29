package com.czy;

import java.io.BufferedInputStream;
import java.io.DataInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.DatagramPacket;
import java.net.InetAddress;
import java.net.Socket;
import java.net.URL;
import java.util.Hashtable;


import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.pm.ActivityInfo;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnTouchListener;
import android.widget.ImageButton;
import android.widget.Toast;
import android.view.WindowManager;
import java.net.DatagramSocket;
import android.widget.*;
public class client extends Activity  {
	private Context mContext = null;
	private ImageView imView = null;
	private Handler messageHandler =null;
	private Boolean isStop = true;
	private String conStr = "http://192.168.10.1:8080/?action=stream";
	private HttpRequest http = null;
	private String cmdPid = "";
	private Button btnSend;
	private ImageButton forward;
	private ImageButton backward;
	private ImageButton left;
	private ImageButton right;
	private ImageButton yforward;
	private ImageButton ybackward;
	private ImageButton yleft;
	private ImageButton yright;
	private ImageButton yres;
	private ImageButton speaker;
	private ImageButton light;
	//////////////////////////////////
	private DatagramSocket socketUDP=null;
	private int       portRemoteNum;
	private int       portLocalNum;	
	private String    addressIP;
	private String    revData;
	private boolean   flag=false;
	private boolean   islighton = false;
	///////////////////////////////////
	private static final int MENU_ITEM_COUNTER = Menu.FIRST; 
	@Override  
	public boolean onCreateOptionsMenu(Menu menu) {   
	    menu.add(0, MENU_ITEM_COUNTER, 0, "连接");   
	    menu.add(0, MENU_ITEM_COUNTER + 1, 0, "设置");   
        return super.onCreateOptionsMenu(menu);   
	}  
	@Override  
	public boolean onOptionsItemSelected(MenuItem item) {   
	    switch (item.getItemId()) {   
	    case MENU_ITEM_COUNTER: 
	    	Conn();	    	
	        break;   
	    case MENU_ITEM_COUNTER + 1:   
	    	Setting();  
	        break;   

	    default:   
	        break;   
	    }   
	    return super.onOptionsItemSelected(item);   
	}  


    /** Called when the activity is first created. */
	@Override
	public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);
        imView = (ImageView)findViewById(R.id.imageView1);	
        forward = (ImageButton)findViewById(R.id.forward);
        forward.setOnTouchListener(new forward_OnTouchListener());
        
        backward = (ImageButton)findViewById(R.id.backward);
        backward.setOnTouchListener(new backward_OnTouchListener()); 
        
        left = (ImageButton)findViewById(R.id.left);
        left.setOnTouchListener(new left_OnTouchListener());
        
        right = (ImageButton)findViewById(R.id.right);
        right.setOnTouchListener(new right_OnTouchListener()); 
        
        yforward = (ImageButton)findViewById(R.id.yforward);
        yforward.setOnTouchListener(new yforward_OnTouchListener());
        
        ybackward = (ImageButton)findViewById(R.id.ybackward);
        ybackward.setOnTouchListener(new ybackward_OnTouchListener()); 
        
        yleft = (ImageButton)findViewById(R.id.yleft);
        yleft.setOnTouchListener(new yleft_OnTouchListener());
        
        yright = (ImageButton)findViewById(R.id.yright);
        yright.setOnTouchListener(new yright_OnTouchListener()); 
               
    	yres = (ImageButton)findViewById(R.id.ImageButton01);
    	yres.setOnTouchListener(new yres_OnTouchListener()); 
        
    	speaker = (ImageButton)findViewById(R.id.ImageButton03);
    	speaker.setOnTouchListener(new speaker_OnTouchListener());
        
    	light = (ImageButton)findViewById(R.id.ImageButton02);
    	light.setOnClickListener(lightClickListener); 
        
		mContext = this;
		http = new HttpRequest();  
        Looper looper = Looper.myLooper();
        messageHandler = new MessageHandler(looper);
    }
    ///////////////////////////////////////////////////
	class forward_OnTouchListener implements OnTouchListener
    {
      @Override
      public boolean onTouch(View v, MotionEvent event)
      {
        if (event.getAction() == MotionEvent.ACTION_DOWN)//按下
           {
        	forward.setImageResource(R.drawable.forward2);
        	String str = "1";	
     	    sendData(str);	     	  
            }  
       if (event.getAction() == MotionEvent.ACTION_UP)//弹起
          {
    	   forward.setImageResource(R.drawable.forward1);
    	   String str = "0";	
    	   sendData(str);	
          }
       //返回true  表示事件处理完毕，会中断系统对该事件的处理。false 系统会同时处理对应的事件
       return true;
      }
    }
 
    ////////////////////////////////////
	class backward_OnTouchListener implements OnTouchListener
    {
      @Override
      public boolean onTouch(View v, MotionEvent event)
      {
        if (event.getAction() == MotionEvent.ACTION_DOWN)//按下
           {
        	backward.setImageResource(R.drawable.backward2);
        	String str = "2";	
     	    sendData(str);		
            }  
       if (event.getAction() == MotionEvent.ACTION_UP)//弹起
          {
    	   backward.setImageResource(R.drawable.backward1);
    	   String str = "0";	
    	   sendData(str);	
          }
       //返回true  表示事件处理完毕，会中断系统对该事件的处理。false 系统会同时处理对应的事件
       return true;
      }
    }
 
    ////////////////////////////////////
	class left_OnTouchListener implements OnTouchListener
    {
      @Override
      public boolean onTouch(View v, MotionEvent event)
      {
        if (event.getAction() == MotionEvent.ACTION_DOWN)//按下
           {
        	left.setImageResource(R.drawable.left2);
        	String str = "3";	
     	    sendData(str);		
            }  
       if (event.getAction() == MotionEvent.ACTION_UP)//弹起
          {
    	   left.setImageResource(R.drawable.left1);
    	   String str = "0";	
    	   sendData(str);	
          }
       //返回true  表示事件处理完毕，会中断系统对该事件的处理。false 系统会同时处理对应的事件
       return true;
      }
    }
 
    ////////////////////////////////////
	class right_OnTouchListener implements OnTouchListener
    {
      @Override
      public boolean onTouch(View v, MotionEvent event)
      {
        if (event.getAction() == MotionEvent.ACTION_DOWN)//按下
           {
        	right.setImageResource(R.drawable.right2);
        	String str = "4";	
     	    sendData(str);		
            }  
       if (event.getAction() == MotionEvent.ACTION_UP)//弹起
          {
    	   right.setImageResource(R.drawable.right1);
    	   String str = "0";	
    	   sendData(str);	
          }
       //返回true  表示事件处理完毕，会中断系统对该事件的处理。false 系统会同时处理对应的事件
       return true;
      }
    }
 
    ////////////////////////////////////
	class yforward_OnTouchListener implements OnTouchListener
    {
      @Override
      public boolean onTouch(View v, MotionEvent event)
      {
        if (event.getAction() == MotionEvent.ACTION_DOWN)//按下
           {
        	yforward.setImageResource(R.drawable.forward2);
        	String str = "5";	
     	    sendData(str);		
            }  
       if (event.getAction() == MotionEvent.ACTION_UP)//弹起
          {
    	   yforward.setImageResource(R.drawable.forward1);
    	   String str = "0";	
    	   sendData(str);	
          }
       //返回true  表示事件处理完毕，会中断系统对该事件的处理。false 系统会同时处理对应的事件
       return true;
      }
    }
 
    ////////////////////////////////////
	class ybackward_OnTouchListener implements OnTouchListener
    {
      @Override
      public boolean onTouch(View v, MotionEvent event)
      {
        if (event.getAction() == MotionEvent.ACTION_DOWN)//按下
           {
        	ybackward.setImageResource(R.drawable.backward2);
        	String str = "6";	
     	    sendData(str);		
            }  
       if (event.getAction() == MotionEvent.ACTION_UP)//弹起
          {
    	   ybackward.setImageResource(R.drawable.backward1);
    	   String str = "0";	
    	   sendData(str);	
          }
       //返回true  表示事件处理完毕，会中断系统对该事件的处理。false 系统会同时处理对应的事件
       return true;
      }
    }
 
    ////////////////////////////////////
	class yleft_OnTouchListener implements OnTouchListener
    {
      @Override
      public boolean onTouch(View v, MotionEvent event)
      {
        if (event.getAction() == MotionEvent.ACTION_DOWN)//按下
           {
        	yleft.setImageResource(R.drawable.left2);
        	String str = "7";	
     	    sendData(str);		
            }  
       if (event.getAction() == MotionEvent.ACTION_UP)//弹起
          {
    	   yleft.setImageResource(R.drawable.left1);
    	   String str = "0";	
    	   sendData(str);	
          }
       //返回true  表示事件处理完毕，会中断系统对该事件的处理。false 系统会同时处理对应的事件
       return true;
      }
    }
 
    ////////////////////////////////////
	class yright_OnTouchListener implements OnTouchListener
    {
      @Override
      public boolean onTouch(View v, MotionEvent event)
      {
        if (event.getAction() == MotionEvent.ACTION_DOWN)//按下
           {
        	yright.setImageResource(R.drawable.right2);
        	String str = "8";	
     	    sendData(str);		
            }  
       if (event.getAction() == MotionEvent.ACTION_UP)//弹起
          {
    	   yright.setImageResource(R.drawable.right1);
    	   String str = "0";	
    	   sendData(str);	
          }
       //返回true  表示事件处理完毕，会中断系统对该事件的处理。false 系统会同时处理对应的事件
       return true;
      }
    }
////////////////////////////////////
class yres_OnTouchListener implements OnTouchListener
{
@Override
public boolean onTouch(View v, MotionEvent event)
{
if (event.getAction() == MotionEvent.ACTION_DOWN)//按下
{
	yres.setImageResource(R.drawable.guion);
String str = "9";	
sendData(str);		
}  
if (event.getAction() == MotionEvent.ACTION_UP)//弹起
{
	yres.setImageResource(R.drawable.guioff);
String str = "0";	
sendData(str);	
}
//返回true  表示事件处理完毕，会中断系统对该事件的处理。false 系统会同时处理对应的事件
return true;
}
}
///////////////////////
private OnClickListener lightClickListener = new OnClickListener() {
	@Override
	public void onClick(View arg0) {
		// TODO Auto-generated method stub				
		if (islighton) 
			{				
				islighton = false;
				light.setImageResource(R.drawable.off);
				String str = "a";	
				sendData(str);				
			}
		else
			{				
				islighton = true;
				light.setImageResource(R.drawable.on);
				String str = "b";	
				sendData(str);						
			}
	}
};	
////////////////////////////////////
class speaker_OnTouchListener implements OnTouchListener
{
@Override
public boolean onTouch(View v, MotionEvent event)
{
if (event.getAction() == MotionEvent.ACTION_DOWN)//按下
{
	speaker.setImageResource(R.drawable.speakeron);
String str = "c";	
sendData(str);		
}  
if (event.getAction() == MotionEvent.ACTION_UP)//弹起
{
	speaker.setImageResource(R.drawable.speakeroff);
String str = "0";	
sendData(str);	
}
//返回true  表示事件处理完毕，会中断系统对该事件的处理。false 系统会同时处理对应的事件
return true;
}
}
    ////////////////////////////////////
	 void sendData(String str)
		{
			 try{
				 flag=true;
				 portRemoteNum=1376;
				 portLocalNum=8080;
		         addressIP = "192.168.10.1";
				 //addressIP = "192.168.1.103";
		         socketUDP = new DatagramSocket(portLocalNum);
				 btnSend.setEnabled(true);
                 } catch (Exception e) {
          // TODO Auto-generated catch block
           e.printStackTrace();}			
			try {
				      		    
			    InetAddress serverAddress = InetAddress.getByName(addressIP);	   
			    byte data [] = str.getBytes(); 
			    DatagramPacket packetS = new DatagramPacket(data,
			    		data.length,serverAddress,portRemoteNum);	
			    //从本地端口给指定IP的远程端口发数据包
			    socketUDP.send(packetS);
			    } catch (Exception e) {
			        // TODO Auto-generated catch block
			        e.printStackTrace();
			    }
			   socketUDP.close(); 
			
		}
	////////////////////////////////////
    class MessageHandler extends Handler {
        public MessageHandler(Looper looper) {
            super(looper);
        }
        public void handleMessage(Message msg) {
        	switch (msg.arg1) {
			case 0:
				imView.setImageBitmap((Bitmap)msg.obj);
				break;
			default:
				break;
			}
        	
        }
    }

	public void Conn() {
		if(isStop){
			isStop = false;
			//conBtn.setText("Clo");
			setTitle("连接"); 
			new Thread() {
	            @SuppressWarnings("unchecked")
				public void run() {
	            	try {
						URL url =new URL(conStr);
						
						Socket server = new Socket(url.getHost(), url.getPort());
						OutputStream os = server.getOutputStream();
						InputStream  is = server.getInputStream();
						
						StringBuffer request = new StringBuffer();
						request.append("GET " + url.getFile() + " HTTP/1.0\r\n");
						request.append("Host: " + url.getHost() + "\r\n");
						request.append("\r\n");
						os.write(request.toString().getBytes(), 0, request.length());

						StreamSplit localStreamSplit = new StreamSplit(new DataInputStream(new BufferedInputStream(is)));
						Hashtable localHashtable = localStreamSplit.readHeaders();
						
						String str3 = (String)localHashtable.get("content-type");
						int n = str3.indexOf("boundary=");
						Object localObject2 = "--";
						if (n != -1){
							localObject2 = str3.substring(n + 9);
							str3 = str3.substring(0, n);
							if (!((String)localObject2).startsWith("--"))
								localObject2 = "--" + (String)localObject2;
						}
						if (str3.startsWith("multipart/x-mixed-replace")){
							localStreamSplit.skipToBoundary((String)localObject2);
						}
						Message message1 = Message.obtain();
						message1.arg1 = 1;
					    messageHandler.sendMessage(message1);
						do{
							if (localObject2 != null){
								localHashtable = localStreamSplit.readHeaders();
								if (localStreamSplit.isAtStreamEnd())
									break;
								str3 = (String)localHashtable.get("content-type");
								if (str3 == null)
									throw new Exception("No part content type");
							}
							if (str3.startsWith("multipart/x-mixed-replace")){
								n = str3.indexOf("boundary=");
								localObject2 = str3.substring(n + 9);
								localStreamSplit.skipToBoundary((String)localObject2);
							}else{
								byte[] localObject3 = localStreamSplit.readToBoundary((String)localObject2);
								if (localObject3.length == 0)
									break;
								
								Message message = Message.obtain();
								message.arg1 = 0;
							    message.obj = BitmapFactory.decodeByteArray(localObject3, 0, localObject3.length);
							    messageHandler.sendMessage(message);
							}
						    try{
						      Thread.sleep(10L);
						    }catch (InterruptedException localInterruptedException){
						    	
						    }
						}while (!isStop);
						server.close();
					} catch (Exception e) {
						e.printStackTrace();
						System.out.println("错误");
						Toast.makeText(mContext, "无法连接上服务器!", Toast.LENGTH_SHORT).show(); 
						Message message = Message.obtain();
						message.arg1 = 1;
					    messageHandler.sendMessage(message);
					}
	            }
			}.start();
		}else{
			isStop = true;
			//conBtn.setText("Con");
			setTitle("断开"); 
		}
	} 
    public void Setting() {
		LayoutInflater factory=LayoutInflater.from(mContext);
		final View v1=factory.inflate(R.layout.setting,null);
		AlertDialog.Builder dialog=new AlertDialog.Builder(mContext);
		dialog.setTitle("连接地址");
		dialog.setView(v1);
		EditText et = (EditText)v1.findViewById(R.id.connectionurl);
    	et.setText(conStr);
        dialog.setPositiveButton("确定", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int whichButton) {
            	EditText qet = (EditText)v1.findViewById(R.id.connectionurl);
            	conStr = qet.getText().toString();
            	Toast.makeText(mContext, "设置成功!", Toast.LENGTH_SHORT).show(); 
            }
        });
        dialog.setNegativeButton("取消",new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int which) {
				
			}
		});
        dialog.show();
	}
    
}