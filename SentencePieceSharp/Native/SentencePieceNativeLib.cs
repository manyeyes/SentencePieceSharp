// The following encoding methods come from:
// https://github.com/Foorcee/SentencePiece.NET
// newly add decode and decode_pieces methods
using System.Runtime.InteropServices;

namespace SentencePieceSharp.Native;

internal partial class SentencePieceNativeLib
{
    private const string DllName = "libSentencepiece";
    
    [LibraryImport(DllName)]
    public static partial IntPtr spp_create();
    
    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    public static partial int spp_load(IntPtr processor, string modelFile);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    public static partial int spp_encode(IntPtr processor, string input, out IntPtr output);

    [LibraryImport(DllName)]
    public static partial void spp_free_array(IntPtr array);

    [LibraryImport(DllName)]
    public static partial void spp_destroy(IntPtr processor);

    [LibraryImport(DllName)]
    public static partial int spp_get_piece_size(IntPtr processor);
    
    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    public static partial int spp_piece_to_id(IntPtr processor, string piece);
    
    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    public static partial IntPtr spp_id_to_piece(IntPtr processor, int pieceId);
    
    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool spp_is_unknown(IntPtr processor, int pieceId);
    
    [LibraryImport(DllName)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool spp_is_control(IntPtr processor, int pieceId);
    
    [LibraryImport(DllName)]
    public static partial int spp_eos_id(IntPtr processor);
    
    [LibraryImport(DllName)]
    public static partial int spp_bos_id(IntPtr processor);
    
    [LibraryImport(DllName)]
    public static partial int spp_pad_id(IntPtr processor);
    
    [LibraryImport(DllName)]
    public static partial int spp_unk_id(IntPtr processor);
    
    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    public static partial void spp_set_encode_extra_options(IntPtr processor, string extraOptions);

    // Add decoding method

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    public static partial int spp_decode(IntPtr processor, [In] int[] input, int input_length, out IntPtr output);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    public static partial void spp_free_string(IntPtr str);

    [LibraryImport(DllName, StringMarshalling = StringMarshalling.Utf8)]
    public static partial int spp_decode_pieces(IntPtr processor, [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] input, int input_length, out IntPtr output);


}