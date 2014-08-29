package com.czy;

import android.os.Handler;
import android.os.Message;
import android.widget.EditText;

import java.io.IOException;
import java.net.*;
import java.util.Date;
import java.text.SimpleDateFormat;

public class udpthread implements Runnable
{
	private EditText etshowrdata = null;
	private String sRecvData;
	private Thread rthread = null;// 接收数据线程
	private DatagramSocket sUdp = null;// 发送数据Socket
	private DatagramSocket rUdp = null;// 接收数据Socket
	private DatagramPacket sPacket = null;
	private DatagramPacket rPacket = null;
	private String remoteIP;
	private int remotePort, localPort;
	private boolean isSHex = false;
	private boolean isRHex = false;
	private String currentSCodes = "UTF-8";
	private String currentRCodes = "UTF-8";
	private byte[] rBuffer = new byte[1024];//接收数据缓存1024字节
	private byte[] fBuffer = null;
	private byte[] sBuffer = null;
	
	private SimpleDateFormat sdf = new SimpleDateFormat("HH:mm:ss");
	
	private Handler vhandler = new Handler()
	{
		@Override
		public void handleMessage(Message msg)
		{
			if (etshowrdata != null)
			{
				if (sRecvData==null) return;
				StringBuilder sb = new StringBuilder();
				sb.append(etshowrdata.getText().toString().trim());
				sb.append("\n");
				sb.append(sRecvData);
				etshowrdata.setText(sb.toString().trim());
				sb.delete(0, sb.length());
				sb = null;
			}
			// super.handleMessage(msg);
		}
	};
	
	public udpthread(EditText et)
	{
		etshowrdata = et;
	}
	
	public void setRemoteIP(String remoteIP)
	{
		this.remoteIP = remoteIP;
	}
	
	public void setRemotePort(int remotePort)
	{
		this.remotePort = remotePort;
	}
	
	public void setLocalPort(int localPort)
	{
		this.localPort = localPort;
	}
	
	public void setSHex(boolean isSHex)
	{
		this.isSHex = isSHex;
	}
	
	public void setRHex(boolean isRHex)
	{
		this.isRHex = isRHex;
	}
	
	public void setCurrentSCodes(String currentSCodes)
	{
		this.currentSCodes = currentSCodes;
	}

	public void setCurrentRCodes(String currentRCodes)
	{
		this.currentRCodes = currentRCodes;
	}

	private void startThread()
	{
		if (rthread == null)
		{
			rthread = new Thread(this);
			rthread.start();
		}
	}
	
	private void stopThread()
	{
		if (rthread != null)
		{
			rthread.stop();
			rthread = null;
		}
	}
	
	@Override
	public void run()
	{
		while (Thread.currentThread() == rthread)
		{
			try
			{
				recvData();
				vhandler.sendEmptyMessage(0);
				Thread.sleep(100);
			} catch (InterruptedException e)
			{
				e.printStackTrace();
			}
		}
	}
	
	private String recvHexData(int len)
	{
		fBuffer = new byte[len];
		System.arraycopy(rBuffer, 0, fBuffer, 0, len);
		StringBuilder sb = new StringBuilder(fBuffer.length);
		String rHex = "";
		for (int i = 0; i < fBuffer.length; i++)
		{
			rHex = Integer.toHexString(fBuffer[i] & 0xFF);
			if (rHex.length() == 1)
				rHex = "0" + rHex;
			sb.append(rHex.toUpperCase());
		}
		return sb.toString().trim();
	}
	
	private void recvData()
	{
		try
		{
			rUdp.receive(rPacket);
			if (isRHex)
				sRecvData = recvHexData(rPacket.getLength());
			else
				sRecvData = new String(rPacket.getData(),currentRCodes).trim();
			sRecvData = String.format("[%s:%d//%s]%s", rPacket.getAddress().getHostAddress(), rPacket.getPort(), sdf.format(new Date()), sRecvData);
		} catch (IOException ie)
		{
			System.out.println("recvdata error:" + ie.getMessage());
			//Toast.makeText(ct, "接收数据错误:" + ie.getMessage(), Toast.LENGTH_LONG);
		}
	}
	
	private boolean CharInRange(char c)
	{
		boolean result = false;
		if (c >= '0' && c <= '9')
			result = true;
		if (c >= 'a' && c <= 'f')
			result = true;
		if (c >= 'A' && c <= 'F')
			result = true;
		return result;
	}
	
	private byte StrToByte(String s)
	{
		return Integer.valueOf(String.valueOf(Integer.parseInt(s, 16))).byteValue();
	}
	
	private void SendHexData(String SData) 
	{
		int datalen = SData.getBytes().length;
		int bytelen = 0;
		if ((datalen % 2)==0)
			bytelen = (int)(datalen / 2);
		else
			bytelen = (int)(datalen / 2) + 1;
		
		sBuffer = new byte[bytelen];
		int i = 0, j = 0;
		while (i < datalen)
		{
			while (i >= 0 && (!CharInRange(SData.charAt(i))))
				i++;
			
			if (((i + 1) >= datalen) || (!CharInRange(SData.charAt(i + 1))))
			{
				sBuffer[j] = StrToByte(String.valueOf(SData.charAt(i)));
				j++;
			} else
			{
				sBuffer[j] = StrToByte(SData.substring(i, i + 2));
				j++;
			}
			
			i += 2;
		}
	}
	
	public void SendData(String SData)
	{
		try
		{
			if (sUdp==null)
				sUdp = new DatagramSocket();
			if (isSHex)
				SendHexData(SData);
			else
				sBuffer = SData.getBytes(currentSCodes);
			sPacket = new DatagramPacket(sBuffer,sBuffer.length,InetAddress.getByName(remoteIP),remotePort);
			sUdp.send(sPacket);
			sUdp.close();
			sUdp = null;
			sPacket = null;
		}catch(IOException ie)
		{
			sUdp.close();
			sUdp = null;
			sPacket = null;
			System.out.println("senddata error:" + ie.getMessage());
		}
	}
	
	public boolean ConnectSocket()
	{
		boolean result = false;
		try
		{
			if (rUdp == null)
				rUdp = new DatagramSocket(localPort);
			if (rPacket == null)
				rPacket = new DatagramPacket(rBuffer, rBuffer.length);
			startThread();
			result = true;
		} catch (SocketException se)
		{
			DisConnectSocket();
			System.out.println("open udp port error:" + se.getMessage());
		}
		return result;
	}
	
	public void DisConnectSocket()
	{
		if (rUdp != null)
		{
			rUdp.close();
			rUdp = null;
		}
		if (rPacket != null)
			rPacket = null;
		stopThread();
	}
	
	/*
	 * public static String getIp() { String localip=null; String netip=null;
	 * try { Enumeration<NetworkInterface> netInterfaces =
	 * NetworkInterface.getNetworkInterfaces(); InetAddress ip = null; boolean
	 * finded=false; while(netInterfaces.hasMoreElements() && !finded) {
	 * NetworkInterface ni=netInterfaces.nextElement(); Enumeration<InetAddress>
	 * address=ni.getInetAddresses(); while(address.hasMoreElements()) {
	 * ip=address.nextElement(); if( !ip.isSiteLocalAddress() &&
	 * !ip.isLoopbackAddress() && ip.getHostAddress().indexOf(":")==-1) {
	 * netip=ip.getHostAddress(); finded=true; break; } else
	 * if(ip.isSiteLocalAddress() && !ip.isLoopbackAddress() &&
	 * ip.getHostAddress().indexOf(":")==-1) { localip=ip.getHostAddress(); } }
	 * } } catch (SocketException e) { e.printStackTrace(); } if(netip!=null &&
	 * !"".equals(netip)) { return netip; }else { return localip; } }
	 */
}
