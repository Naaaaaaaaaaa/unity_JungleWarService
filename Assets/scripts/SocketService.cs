/**====================================
 *Copyright(C) 2018 by Wipace 
 *All rights reserved. 
 *FileName:     .cs 
 *Author:       CGzhao 
 *Version:      1.0 
 *UnityVersion：2018.2.3 
 *Date:         2018-11-27 
 *Email:		1341674064@qq.com
 *Description:    
 *History: 
======================================**/

using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class SocketService : MonoBehaviour
{
	/// <summary>
	/// 服务器IP地址
	/// </summary>
	private string ip = "172.27.44.193";
	/// <summary>
	/// 申请的端口号
	/// </summary>
	private int port  = 6666;
	/// <summary>
	/// 处理连接的队列数
	/// </summary>
	private int capacity = 10;
	/// <summary>
	/// socket服务器
	/// </summary>
	private Socket serviceSocket;
	/// <summary>
	/// ip地址
	/// </summary>
	private IPAddress ipAddress;
	/// <summary>
	/// ip + 端口号
	/// </summary>
	private IPEndPoint ipEndPoint;
	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="url">ip地址</param>
	public SocketService()
	{
		serviceSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		//本机ip: 172.27.44.193
		//两种方法：
		//_ipAddress = new IPAddress(new byte[]{172, 27, 44, 193});
		ipAddress = IPAddress.Parse(this.ip);
		ipEndPoint = new IPEndPoint(ipAddress, this.port);
		//绑定端口号
		serviceSocket.Bind(ipEndPoint);
		//申请监听的队列
		serviceSocket.Listen(capacity);
		//接受一个客户端连接
		Socket clientSocket = serviceSocket.Accept();
		
		//向客户端发送一条消息
		string msg = "连接到服务器";
		//将一个字符串转化成byte数组
		byte[] info = System.Text.Encoding.UTF8.GetBytes(msg);
		clientSocket.Send(info);
		
		//接收客户端的一条消息
		byte[] dataBuffer = new byte[1024];
		//接收到的字节数长度
		int count = clientSocket.Receive(dataBuffer);
		//读取接收到的信息
		string msgReceive = System.Text.Encoding.UTF8.GetString(dataBuffer, 0, count);
		Debug.Log(msgReceive);
		
		//关闭跟客户端的连接
		clientSocket.Close();
		//关闭服务器
		serviceSocket.Close();
	}
}
