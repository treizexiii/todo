// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: message.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace GrpcMessage {

  /// <summary>Holder for reflection information generated from message.proto</summary>
  public static partial class MessageReflection {

    #region Descriptor
    /// <summary>File descriptor for message.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MessageReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg1tZXNzYWdlLnByb3RvEhJHcnBjTWVzc2FnZS5Db21tb24aGWdvb2dsZS9w",
            "cm90b2J1Zi9hbnkucHJvdG8aH2dvb2dsZS9wcm90b2J1Zi90aW1lc3RhbXAu",
            "cHJvdG8itAEKCFJlc3BvbnNlEg8KB3N1Y2Nlc3MYASABKAgSDAoEY29kZRgC",
            "IAEoBRIPCgdtZXNzYWdlGAMgASgJEi8KC2V4ZWN1dGVUaW1lGAQgASgLMhou",
            "Z29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcBIiCgRkYXRhGAUgASgLMhQuZ29v",
            "Z2xlLnByb3RvYnVmLkFueRIjCgVkYXRhcxgGIAMoCzIULmdvb2dsZS5wcm90",
            "b2J1Zi5BbnlCDqoCC0dycGNNZXNzYWdlYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Google.Protobuf.WellKnownTypes.AnyReflection.Descriptor, global::Google.Protobuf.WellKnownTypes.TimestampReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::GrpcMessage.Response), global::GrpcMessage.Response.Parser, new[]{ "Success", "Code", "Message", "ExecuteTime", "Data", "Datas" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class Response : pb::IMessage<Response>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Response> _parser = new pb::MessageParser<Response>(() => new Response());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Response> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::GrpcMessage.MessageReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Response() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Response(Response other) : this() {
      success_ = other.success_;
      code_ = other.code_;
      message_ = other.message_;
      executeTime_ = other.executeTime_ != null ? other.executeTime_.Clone() : null;
      data_ = other.data_ != null ? other.data_.Clone() : null;
      datas_ = other.datas_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Response Clone() {
      return new Response(this);
    }

    /// <summary>Field number for the "success" field.</summary>
    public const int SuccessFieldNumber = 1;
    private bool success_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Success {
      get { return success_; }
      set {
        success_ = value;
      }
    }

    /// <summary>Field number for the "code" field.</summary>
    public const int CodeFieldNumber = 2;
    private int code_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int Code {
      get { return code_; }
      set {
        code_ = value;
      }
    }

    /// <summary>Field number for the "message" field.</summary>
    public const int MessageFieldNumber = 3;
    private string message_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Message {
      get { return message_; }
      set {
        message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "executeTime" field.</summary>
    public const int ExecuteTimeFieldNumber = 4;
    private global::Google.Protobuf.WellKnownTypes.Timestamp executeTime_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Google.Protobuf.WellKnownTypes.Timestamp ExecuteTime {
      get { return executeTime_; }
      set {
        executeTime_ = value;
      }
    }

    /// <summary>Field number for the "data" field.</summary>
    public const int DataFieldNumber = 5;
    private global::Google.Protobuf.WellKnownTypes.Any data_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::Google.Protobuf.WellKnownTypes.Any Data {
      get { return data_; }
      set {
        data_ = value;
      }
    }

    /// <summary>Field number for the "datas" field.</summary>
    public const int DatasFieldNumber = 6;
    private static readonly pb::FieldCodec<global::Google.Protobuf.WellKnownTypes.Any> _repeated_datas_codec
        = pb::FieldCodec.ForMessage(50, global::Google.Protobuf.WellKnownTypes.Any.Parser);
    private readonly pbc::RepeatedField<global::Google.Protobuf.WellKnownTypes.Any> datas_ = new pbc::RepeatedField<global::Google.Protobuf.WellKnownTypes.Any>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::Google.Protobuf.WellKnownTypes.Any> Datas {
      get { return datas_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Response);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Response other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Success != other.Success) return false;
      if (Code != other.Code) return false;
      if (Message != other.Message) return false;
      if (!object.Equals(ExecuteTime, other.ExecuteTime)) return false;
      if (!object.Equals(Data, other.Data)) return false;
      if(!datas_.Equals(other.datas_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Success != false) hash ^= Success.GetHashCode();
      if (Code != 0) hash ^= Code.GetHashCode();
      if (Message.Length != 0) hash ^= Message.GetHashCode();
      if (executeTime_ != null) hash ^= ExecuteTime.GetHashCode();
      if (data_ != null) hash ^= Data.GetHashCode();
      hash ^= datas_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Success != false) {
        output.WriteRawTag(8);
        output.WriteBool(Success);
      }
      if (Code != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Code);
      }
      if (Message.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Message);
      }
      if (executeTime_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(ExecuteTime);
      }
      if (data_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(Data);
      }
      datas_.WriteTo(output, _repeated_datas_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Success != false) {
        output.WriteRawTag(8);
        output.WriteBool(Success);
      }
      if (Code != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Code);
      }
      if (Message.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Message);
      }
      if (executeTime_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(ExecuteTime);
      }
      if (data_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(Data);
      }
      datas_.WriteTo(ref output, _repeated_datas_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Success != false) {
        size += 1 + 1;
      }
      if (Code != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Code);
      }
      if (Message.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
      }
      if (executeTime_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ExecuteTime);
      }
      if (data_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Data);
      }
      size += datas_.CalculateSize(_repeated_datas_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Response other) {
      if (other == null) {
        return;
      }
      if (other.Success != false) {
        Success = other.Success;
      }
      if (other.Code != 0) {
        Code = other.Code;
      }
      if (other.Message.Length != 0) {
        Message = other.Message;
      }
      if (other.executeTime_ != null) {
        if (executeTime_ == null) {
          ExecuteTime = new global::Google.Protobuf.WellKnownTypes.Timestamp();
        }
        ExecuteTime.MergeFrom(other.ExecuteTime);
      }
      if (other.data_ != null) {
        if (data_ == null) {
          Data = new global::Google.Protobuf.WellKnownTypes.Any();
        }
        Data.MergeFrom(other.Data);
      }
      datas_.Add(other.datas_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Success = input.ReadBool();
            break;
          }
          case 16: {
            Code = input.ReadInt32();
            break;
          }
          case 26: {
            Message = input.ReadString();
            break;
          }
          case 34: {
            if (executeTime_ == null) {
              ExecuteTime = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(ExecuteTime);
            break;
          }
          case 42: {
            if (data_ == null) {
              Data = new global::Google.Protobuf.WellKnownTypes.Any();
            }
            input.ReadMessage(Data);
            break;
          }
          case 50: {
            datas_.AddEntriesFrom(input, _repeated_datas_codec);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Success = input.ReadBool();
            break;
          }
          case 16: {
            Code = input.ReadInt32();
            break;
          }
          case 26: {
            Message = input.ReadString();
            break;
          }
          case 34: {
            if (executeTime_ == null) {
              ExecuteTime = new global::Google.Protobuf.WellKnownTypes.Timestamp();
            }
            input.ReadMessage(ExecuteTime);
            break;
          }
          case 42: {
            if (data_ == null) {
              Data = new global::Google.Protobuf.WellKnownTypes.Any();
            }
            input.ReadMessage(Data);
            break;
          }
          case 50: {
            datas_.AddEntriesFrom(ref input, _repeated_datas_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
