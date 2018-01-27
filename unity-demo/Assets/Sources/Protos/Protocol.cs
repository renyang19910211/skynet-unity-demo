// Generated by sprotodump. DO NOT EDIT!
using System;
using Sproto;
using System.Collections.Generic;

public class Protocol : ProtocolBase {
	public static  Protocol Instance = new Protocol();
	private Protocol() {
		Protocol.SetProtocol<get> (get.Tag);
		Protocol.SetRequest<SprotoType.get.request> (get.Tag);
		Protocol.SetResponse<SprotoType.get.response> (get.Tag);

		Protocol.SetProtocol<handshake> (handshake.Tag);
		Protocol.SetResponse<SprotoType.handshake.response> (handshake.Tag);

		Protocol.SetProtocol<heartbeat> (heartbeat.Tag);

		Protocol.SetProtocol<quit> (quit.Tag);

		Protocol.SetProtocol<register_account> (register_account.Tag);
		Protocol.SetRequest<SprotoType.register_account.request> (register_account.Tag);
		Protocol.SetResponse<SprotoType.register_account.response> (register_account.Tag);

		Protocol.SetProtocol<set> (set.Tag);
		Protocol.SetRequest<SprotoType.set.request> (set.Tag);

		Protocol.SetProtocol<test_msg> (test_msg.Tag);
		Protocol.SetRequest<SprotoType.test_msg.request> (test_msg.Tag);

	}

	public class get {
		public const int Tag = 2;
	}

	public class handshake {
		public const int Tag = 1;
	}

	public class heartbeat {
		public const int Tag = 6;
	}

	public class quit {
		public const int Tag = 4;
	}

	public class register_account {
		public const int Tag = 5;
	}

	public class set {
		public const int Tag = 3;
	}

	public class test_msg {
		public const int Tag = 7;
	}

}