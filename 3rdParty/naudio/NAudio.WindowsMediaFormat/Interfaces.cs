﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NAudio.WindowsMediaFormat
{
    // wmsdkidl.h
    [Guid("96406BD6-2B2B-11d3-B36B-00C04F6108FF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMReader
    {
        uint Close();
        
        uint GetOutputCount(
            [Out] out uint pcOutputs);
        
        uint GetOutputFormat(
            [In] uint dwOutputNumber, 
            [In] uint dwFormatNumber, 
            [Out] out IWMOutputMediaProps ppProps);
        
        uint GetOutputFormatCount(
            [In] uint dwOutputNumber, 
            [Out] out uint pcFormats);
        
        uint GetOutputProps(
            [In] uint dwOutputNum, 
            [Out] out IWMOutputMediaProps ppOutput);
        
        uint Open(
            [In,MarshalAs(UnmanagedType.LPWStr)] string url, 
            [In] IWMReaderCallback pCallback, 
            IntPtr pvContext);
        
        uint Pause();
        
        uint Resume();
        
        uint SetOutputProps(
            [In] uint dwOutputNum, 
            [In] IWMOutputMediaProps pOutput);
        
        //WM_START_CURRENTPOSITION = -1
        uint Start(
            [In] long cnsStart, 
            [In] long cnsDuration, 
            [In] float fRate, 
            [In] IntPtr pvContext);
        
        uint Stop();
    }

    // wmsdkidl.h
    [Guid("96406BD8-2B2B-11d3-B36B-00C04F6108FF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMReaderCallback : IWMStatusCallback
    {
        uint OnSample(
            [In] uint dwOutputNum, 
            [In] long cnsSampleTime,
            [In] long cnsSampleDuration,
            [In] uint dwFlags,
            [In] INSSBuffer pSample,
            [In] IntPtr pvContext);

    }

    // wmsbuffer.h
    [Guid("E1CD3524-03D7-11d2-9EED-006097D2D7CF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface INSSBuffer
    {
        uint GetLength(
            [Out] out uint pdwLength);
        uint SetLength(
            [In] uint dwLength);
        uint GetMaxLength(
            [Out] uint pdwLength);
        uint GetBuffer(
            [Out] out byte[] ppdwBuffer); // BYTE **ppdwBuffer
        uint GetBufferAndLength(
            [Out] out byte[] ppdwBuffer, 
            [Out] out uint pdwLength);
    }

    // wmsdkidl.h
    [Guid("96406BD7-2B2B-11d3-B36B-00C04F6108FF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMOutputMediaProps : IWMMediaProps
    {
        uint GetConnectionName(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszName, 
            [In, Out] ref ushort pcchName);
        uint GetStreamGroupName(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszName, 
            [In, Out] ref ushort pcchName);
    }

    // wmsdkidl.h
    [Guid("96406BD5-2B2B-11d3-B36B-00C04F6108FF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMInputMediaProps : IWMMediaProps
    {
        uint GetConnectionName(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszName, 
            [In, Out] ref ushort pcchName);

        uint GetGroupName(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszName, 
            [In, Out] ref ushort pcchName);
    };

    // wmsdkidl.h
    [Guid("96406BCE-2B2B-11d3-B36B-00C04F6108FF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMMediaProps
    {
        uint GetType([Out] out Guid pguidType);
        
        uint GetMediaType([Out] out WM_MEDIA_TYPE pType,
            [Out, In] uint pcbType);
        
        uint SetMediaType([In] WM_MEDIA_TYPE pType);
    }

    [Guid("9397F121-7705-4dc9-B049-98B698188414"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMSyncReader
    {
        uint Open(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszFilename);

        uint Close();

        uint SetRange(
            [In] long cnsStartTime, 
            [In] long cnsDuration);

        uint SetRangeByFrame(
            [In] ushort wStreamNum, 
            [In] long qwFrameNumber, 
            [In] long cFramesToRead);

        uint GetNextSample(
            [In] ushort wStreamNum,
            [Out] out INSSBuffer ppSample,
            [Out] out long pcnsSampleTime,
            [Out] out long pcnsDuration,
            [Out] out uint pdwFlags,
            [Out] out uint pdwOutputNum,
            [Out] out ushort pwStreamNum);

        uint SetStreamsSelected(
            [In] ushort cStreamCount,
            [In] ushort[] pwStreamNumbers,
            [In] WMT_STREAM_SELECTION[] pSelections);

        uint GetStreamSelected( 
            [In] ushort wStreamNum,
            [Out] out WMT_STREAM_SELECTION pSelection);

        uint SetReadStreamSamples( 
            [In] ushort wStreamNum,
            [In] bool fCompressed);

        uint GetReadStreamSamples( 
            [In] ushort wStreamNum,
            [Out] out bool pfCompressed);

        uint GetOutputSetting( 
            [In] uint dwOutputNum,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszName,
            [Out] out WMT_ATTR_DATATYPE pType,
            [Out, In] ref byte[] pValue, // BYTE *
            [Out, In] ref ushort pcbLength);

        uint SetOutputSetting( 
            [In] uint dwOutputNum,
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszName,
            [In] WMT_ATTR_DATATYPE Type,
            [In] byte[] pValue,
            [In] ushort cbLength);

        uint GetOutputCount( 
            [Out] out uint pcOutputs);

        uint GetOutputProps( 
            [In] uint dwOutputNum,
            [Out] out IWMOutputMediaProps ppOutput);

        uint SetOutputProps( 
            [In] uint dwOutputNum,
            [In] IWMOutputMediaProps pOutput);

        uint GetOutputFormatCount( 
            [In] uint dwOutputNum,
            [Out] out uint pcFormats);

        uint GetOutputFormat( 
            [In] uint dwOutputNum,
            [In] uint dwFormatNum,
            [Out] out IWMOutputMediaProps ppProps);

        uint GetOutputNumberForStream( 
            [In] ushort wStreamNum,
            [Out] out uint pdwOutputNum);

        uint GetStreamNumberForOutput( 
            [In] uint dwOutputNum,
            [Out] out ushort pwStreamNum);

        uint GetMaxOutputSampleSize( 
            [In] uint dwOutput,
            [Out] out uint pcbMax);

        uint GetMaxStreamSampleSize( 
            [In] ushort wStream,
            [Out] out uint pcbMax);

        uint OpenStream([In] object pStream); // need to find where IStream is defined
    }

    // wmsdkidl.h
    [Guid("6d7cdc70-9888-11d3-8edc-00c04f6109cf"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMStatusCallback
    {
        uint OnStatus( 
            [In] WMT_STATUS Status,
            [In] uint hr,
            [In] WMT_ATTR_DATATYPE dwType,
            [In] byte[] pValue, // BYTE *
            [In] IntPtr pvContext);
    };

    [Guid("96406BD4-2B2B-11d3-B36B-00C04F6108FF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMWriter
    {
        uint SetProfileByID( 
            [In] ref Guid guidProfile); // REFGUID

        uint SetProfile( 
            [In] IWMProfile pProfile);

        uint SetOutputFilename( 
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszFilename);

        uint GetInputCount( 
            [Out] out int pcInputs); // DWORD *

        uint GetInputProps( 
            [In] uint dwInputNum,
            [Out] out IWMInputMediaProps ppInput);

        uint SetInputProps( 
            [In] uint dwInputNum,
            [In] IWMInputMediaProps pInput);

        uint GetInputFormatCount( 
            [In] uint dwInputNumber,
            [Out] out uint pcFormats);

        uint GetInputFormat( 
            [In] uint dwInputNumber,
            [In] uint dwFormatNumber,
            [Out] out IWMInputMediaProps pProps);

        uint BeginWriting();

        uint EndWriting();

        uint AllocateSample( 
            [In] uint dwSampleSize,
            [Out] out INSSBuffer ppSample);

        uint WriteSample( 
            [In] uint dwInputNum,
            [In] long cnsSampleTime,
            [In] uint dwFlags,
            [In] INSSBuffer pSample);
        
        uint Flush();
    };

    [Guid("96406BDB-2B2B-11d3-B36B-00C04F6108FF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMProfile
    {
        uint GetVersion( 
            [Out] out WMT_VERSION pdwVersion);
        
        uint GetName( 
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszName, 
            [Out, In] ref uint pcchName);
        
        uint SetName( 
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszName);
        
        uint GetDescription( 
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszDescription, 
            [Out, In] ref uint pcchDescription);
        
        uint SetDescription( 
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszDescription);
        
        uint GetStreamCount( 
            [Out] out uint pcStreams);
        
        uint GetStream( 
            [In] uint dwStreamIndex,
            [Out] out IWMStreamConfig ppConfig);
        
        uint GetStreamByNumber( 
            [In] short wStreamNum,
            [Out] out IWMStreamConfig ppConfig);
        
        uint RemoveStream( 
            [In] IWMStreamConfig pConfig);
        
        uint RemoveStreamByNumber( 
            [In] short wStreamNum);
        
        uint AddStream( 
            [In] IWMStreamConfig pConfig);
        
        uint ReconfigStream( 
            [In] IWMStreamConfig pConfig);
        
        uint CreateNewStream( 
            [In] ref Guid guidStreamType,
            [Out] out IWMStreamConfig ppConfig);
        
        uint GetMutualExclusionCount( 
            [Out] out uint pcME);
        
        uint GetMutualExclusion( 
            [In] uint dwMEIndex,
            [Out] out IWMMutualExclusion ppME);
        
        uint RemoveMutualExclusion( 
            [In] IWMMutualExclusion pME);
        
        uint AddMutualExclusion( 
            [In] IWMMutualExclusion pME);
        
        uint CreateNewMutualExclusion( 
            [Out] out IWMMutualExclusion ppME);
    };

    [Guid("96406BDE-2B2B-11d3-B36B-00C04F6108FF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMMutualExclusion : IWMStreamList
    {
        uint GetType( 
            [Out] out Guid pguidType);
        
        uint SetType( 
            [In] ref Guid guidType);
    };

    [Guid("96406BDD-2B2B-11d3-B36B-00C04F6108FF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMStreamList
    {
        uint GetStreams( 
            [Out] ushort[] pwStreamNumArray,
            [Out, In] ref short pcStreams);
        
        uint AddStream( 
            [In] ushort wStreamNum);
        
        uint RemoveStream( 
            [In] ushort wStreamNum);
        
    };

    [Guid("96406BDC-2B2B-11d3-B36B-00C04F6108FF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMStreamConfig
    {
        uint GetStreamType( 
            [Out] out Guid pguidStreamType);
        
        uint GetStreamNumber( 
            [Out] out ushort pwStreamNum);
        
        uint SetStreamNumber( 
            [In] ushort wStreamNum);
        
        uint GetStreamName(
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszStreamName, 
            [Out, In] ref ushort pcchStreamName);
        
        uint SetStreamName( 
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszStreamName);
        
        uint GetConnectionName( 
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszInputName, 
            [Out, In] ref ushort pcchInputName);
        
        uint SetConnectionName( 
            [In, MarshalAs(UnmanagedType.LPWStr)] string pwszInputName);
        
        uint GetBitrate( 
            [Out] out uint pdwBitrate);
        
        uint SetBitrate( 
            [In] uint pdwBitrate);
        
        uint GetBufferWindow( 
            [Out] out uint pmsBufferWindow);
        
        uint SetBufferWindow( 
            [In] uint msBufferWindow);
    };
}
