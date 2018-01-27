// Generated by sprotodump. DO NOT EDIT!
// source: gen_example/TestDecAndBinary.sproto

using System;
using Sproto;
using System.Collections.Generic;

namespace SprotoType { 
	public class Foor : SprotoTypeBase {
		private static int max_field_count = 3;
		
		
		private Dictionary<double, TestData> _test1; // tag 1
		public Dictionary<double, TestData> test1 {
			get { return _test1; }
			set { base.has_field.set_field (0, true); _test1 = value; }
		}
		public bool HasTest1 {
			get { return base.has_field.has_field (0); }
		}

		private List<TestData> _test2; // tag 2
		public List<TestData> test2 {
			get { return _test2; }
			set { base.has_field.set_field (1, true); _test2 = value; }
		}
		public bool HasTest2 {
			get { return base.has_field.has_field (1); }
		}

		public Foor () : base(max_field_count) {}

		public Foor (byte[] buffer) : base(max_field_count, buffer) {
			this.decode ();
		}

		protected override void decode () {
			int tag = -1;
			while (-1 != (tag = base.deserialize.read_tag ())) {
				switch (tag) {
				case 1:
					this.test1 = base.deserialize.read_map<double, TestData>(v => v.v1);
					break;
				case 2:
					this.test2 = base.deserialize.read_obj_list<TestData> ();
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
				base.serialize.write_obj (this.test1, 1);
			}

			if (base.has_field.has_field (1)) {
				base.serialize.write_obj (this.test2, 2);
			}

			return base.serialize.close ();
		}
	}


	public class TestData : SprotoTypeBase {
		private static int max_field_count = 7;
		
		
		private double _v1; // tag 1
		public double v1 {
			get { return _v1; }
			set { base.has_field.set_field (0, true); _v1 = value; }
		}
		public bool HasV1 {
			get { return base.has_field.has_field (0); }
		}

		private List<double> _v2; // tag 2
		public List<double> v2 {
			get { return _v2; }
			set { base.has_field.set_field (1, true); _v2 = value; }
		}
		public bool HasV2 {
			get { return base.has_field.has_field (1); }
		}

		private byte[] _v3; // tag 3
		public byte[] v3 {
			get { return _v3; }
			set { base.has_field.set_field (2, true); _v3 = value; }
		}
		public bool HasV3 {
			get { return base.has_field.has_field (2); }
		}

		private List<byte[]> _v4; // tag 4
		public List<byte[]> v4 {
			get { return _v4; }
			set { base.has_field.set_field (3, true); _v4 = value; }
		}
		public bool HasV4 {
			get { return base.has_field.has_field (3); }
		}

		private Int64 _v5; // tag 5
		public Int64 v5 {
			get { return _v5; }
			set { base.has_field.set_field (4, true); _v5 = value; }
		}
		public bool HasV5 {
			get { return base.has_field.has_field (4); }
		}

		private string _v6; // tag 6
		public string v6 {
			get { return _v6; }
			set { base.has_field.set_field (5, true); _v6 = value; }
		}
		public bool HasV6 {
			get { return base.has_field.has_field (5); }
		}

		public TestData () : base(max_field_count) {}

		public TestData (byte[] buffer) : base(max_field_count, buffer) {
			this.decode ();
		}

		protected override void decode () {
			int tag = -1;
			while (-1 != (tag = base.deserialize.read_tag ())) {
				switch (tag) {
				case 1:
					this.v1 = base.deserialize.read_decimal (10000.0);
					break;
				case 2:
					this.v2 = base.deserialize.read_decimal_list (100.0);
					break;
				case 3:
					this.v3 = base.deserialize.read_binary ();
					break;
				case 4:
					this.v4 = base.deserialize.read_binary_list ();
					break;
				case 5:
					this.v5 = base.deserialize.read_integer ();
					break;
				case 6:
					this.v6 = base.deserialize.read_string ();
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
				base.serialize.write_decimal (this.v1, 10000.0, 1);
			}

			if (base.has_field.has_field (1)) {
				base.serialize.write_decimal (this.v2, 100.0, 2);
			}

			if (base.has_field.has_field (2)) {
				base.serialize.write_binary (this.v3, 3);
			}

			if (base.has_field.has_field (3)) {
				base.serialize.write_binary (this.v4, 4);
			}

			if (base.has_field.has_field (4)) {
				base.serialize.write_integer (this.v5, 5);
			}

			if (base.has_field.has_field (5)) {
				base.serialize.write_string (this.v6, 6);
			}

			return base.serialize.close ();
		}
	}


}

