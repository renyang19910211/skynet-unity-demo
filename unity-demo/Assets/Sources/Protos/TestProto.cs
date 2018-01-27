// Generated by sprotodump. DO NOT EDIT!
// source: ../../../../protos/TestProto.sproto

using System;
using Sproto;
using System.Collections.Generic;

namespace SprotoType { 
	public class chat_msg {
	
		public class request : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private string _msg; // tag 0
			public string msg {
				get { return _msg; }
				set { base.has_field.set_field (0, true); _msg = value; }
			}
			public bool HasMsg {
				get { return base.has_field.has_field (0); }
			}

			public request () : base(max_field_count) {}

			public request (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.msg = base.deserialize.read_string ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_string (this.msg, 0);
				}

				return base.serialize.close ();
			}
		}


	}


	public class handshake {
	
		public class response : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private string _msg; // tag 0
			public string msg {
				get { return _msg; }
				set { base.has_field.set_field (0, true); _msg = value; }
			}
			public bool HasMsg {
				get { return base.has_field.has_field (0); }
			}

			public response () : base(max_field_count) {}

			public response (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.msg = base.deserialize.read_string ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_string (this.msg, 0);
				}

				return base.serialize.close ();
			}
		}


	}


	public class login_account {
	
		public class request : SprotoTypeBase {
			private static int max_field_count = 2;
			
			
			private string _account; // tag 0
			public string account {
				get { return _account; }
				set { base.has_field.set_field (0, true); _account = value; }
			}
			public bool HasAccount {
				get { return base.has_field.has_field (0); }
			}

			private string _password; // tag 1
			public string password {
				get { return _password; }
				set { base.has_field.set_field (1, true); _password = value; }
			}
			public bool HasPassword {
				get { return base.has_field.has_field (1); }
			}

			public request () : base(max_field_count) {}

			public request (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.account = base.deserialize.read_string ();
						break;
					case 1:
						this.password = base.deserialize.read_string ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_string (this.account, 0);
				}

				if (base.has_field.has_field (1)) {
					base.serialize.write_string (this.password, 1);
				}

				return base.serialize.close ();
			}
		}


		public class response : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private bool _result; // tag 0
			public bool result {
				get { return _result; }
				set { base.has_field.set_field (0, true); _result = value; }
			}
			public bool HasResult {
				get { return base.has_field.has_field (0); }
			}

			public response () : base(max_field_count) {}

			public response (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.result = base.deserialize.read_boolean ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_boolean (this.result, 0);
				}

				return base.serialize.close ();
			}
		}


	}


	public class package : SprotoTypeBase {
		private static int max_field_count = 2;
		
		
		private Int64 _type; // tag 0
		public Int64 type {
			get { return _type; }
			set { base.has_field.set_field (0, true); _type = value; }
		}
		public bool HasType {
			get { return base.has_field.has_field (0); }
		}

		private Int64 _session; // tag 1
		public Int64 session {
			get { return _session; }
			set { base.has_field.set_field (1, true); _session = value; }
		}
		public bool HasSession {
			get { return base.has_field.has_field (1); }
		}

		public package () : base(max_field_count) {}

		public package (byte[] buffer) : base(max_field_count, buffer) {
			this.decode ();
		}

		protected override void decode () {
			int tag = -1;
			while (-1 != (tag = base.deserialize.read_tag ())) {
				switch (tag) {
				case 0:
					this.type = base.deserialize.read_integer ();
					break;
				case 1:
					this.session = base.deserialize.read_integer ();
					break;
				default:
					base.deserialize.read_unknow_data ();
					break;
				}
			}
		}

		public override int encode (SprotoStream stream) {
			base.serialize.open (stream);

			if (base.has_field.has_field (0)) {
				base.serialize.write_integer (this.type, 0);
			}

			if (base.has_field.has_field (1)) {
				base.serialize.write_integer (this.session, 1);
			}

			return base.serialize.close ();
		}
	}


	public class register_account {
	
		public class request : SprotoTypeBase {
			private static int max_field_count = 2;
			
			
			private string _account; // tag 0
			public string account {
				get { return _account; }
				set { base.has_field.set_field (0, true); _account = value; }
			}
			public bool HasAccount {
				get { return base.has_field.has_field (0); }
			}

			private string _password; // tag 1
			public string password {
				get { return _password; }
				set { base.has_field.set_field (1, true); _password = value; }
			}
			public bool HasPassword {
				get { return base.has_field.has_field (1); }
			}

			public request () : base(max_field_count) {}

			public request (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.account = base.deserialize.read_string ();
						break;
					case 1:
						this.password = base.deserialize.read_string ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_string (this.account, 0);
				}

				if (base.has_field.has_field (1)) {
					base.serialize.write_string (this.password, 1);
				}

				return base.serialize.close ();
			}
		}


		public class response : SprotoTypeBase {
			private static int max_field_count = 1;
			
			
			private bool _msg; // tag 0
			public bool msg {
				get { return _msg; }
				set { base.has_field.set_field (0, true); _msg = value; }
			}
			public bool HasMsg {
				get { return base.has_field.has_field (0); }
			}

			public response () : base(max_field_count) {}

			public response (byte[] buffer) : base(max_field_count, buffer) {
				this.decode ();
			}

			protected override void decode () {
				int tag = -1;
				while (-1 != (tag = base.deserialize.read_tag ())) {
					switch (tag) {
					case 0:
						this.msg = base.deserialize.read_boolean ();
						break;
					default:
						base.deserialize.read_unknow_data ();
						break;
					}
				}
			}

			public override int encode (SprotoStream stream) {
				base.serialize.open (stream);

				if (base.has_field.has_field (0)) {
					base.serialize.write_boolean (this.msg, 0);
				}

				return base.serialize.close ();
			}
		}


	}


}

