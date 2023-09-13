# UDP 통신

환경 boost/ nlohmann json 

nlohmann json
- 장점 
    직관적인 구문, 손쉬운 적용 

- 단점 
    한글 지원이 완벽하게 호환이 안됨, 바이트 문제로 에러 발생

```c++
class UDPClient

{
public:
	UDPClient(
		boost::asio::io_service& io_service,
		const std::string& host,
		const std::string& port
	) : io_service_(io_service), socket_(io_service, udp::endpoint(udp::v4(), 0)) {
		udp::resolver resolver(io_service_);
		udp::resolver::query query(udp::v4(), host, port);
		udp::resolver::iterator iter = resolver.resolve(query);
		endpoint_ = *iter;
	}

	~UDPClient()
	{
		socket_.close();
	}

	void send(const std::string& msg) {
		socket_.send_to(boost::asio::buffer(msg, msg.size()), endpoint_);
	}

private:
	boost::asio::io_service& io_service_;
	udp::socket socket_;
	udp::endpoint endpoint_;
};

void UdpSend()
{
	if (m_Port)
	{		
		CString IP;
		m_IPEdt.GetWindowText(IP);
		CString Port;
		m_PortEdt.GetWindowText(Port);

		CT2CA pszConvertedAnsiString1(IP);
		std::string ip(pszConvertedAnsiString1);

		CT2CA pszConvertedAnsiString2(Port);
		std::string port(pszConvertedAnsiString2);	

	
		boost::asio::io_service io_service;
			
		UDPClient client(io_service, ip.c_str(), port.c_str());

		TCHAR CurrentBuildNumber[255] = "";
		TCHAR ReleaseId[255] = "";
		TCHAR ProductName[255] = "";

	
		json j;
        

		j["test"] = {
			{"test_1", "test1"},
			{"test2_id", "test2"}															
		};
		//std::cout << j << std::endl;
		
		
		CHAR data[5000] = "";
        
		sprintf_s(data, "%s\n", j.dump().c_str());	
							
		client.send(data);
	}
}

```

