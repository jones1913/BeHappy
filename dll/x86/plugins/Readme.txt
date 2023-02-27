Now the dll's listed here are loaded by BeHappy from this folder, then don't need to be copied to ...\AviSynth 2.5\plugins folder.
Some files are included because are from the BeHappy team, other must be downloaded to actualize.
With this release all bass*.dll are included because problems with name and old versions,
if owners don't authorize this distribution I can delete the files. Remember (1)

There are 2 categories, UTILS:

AudioLimiter.dll         14/03/2006  Help to downmix 7.1 -> 5.1 preserving Front volume. http://forum.doom9.org/showthread.php?p=1722472
soxfilter.dll            03/01/2006  If you want use the Upmix DSP function (or for others many uses). http://forum.doom9.org/showthread.php?p=761154
TimeStretch.dll 2.5.8    16/05/2015  Support for multichannel audio. Need Visual C++ 2010 runtimes. http://forum.doom9.org/showthread.php?p=1722472

And DECODERS:

BassAudio.dll   2.4      14/11/2014  Interface to open Bass Audio libraries v2.4 in AviSynth (included)
bass.dll        2.4.12.1 18/04/2016  Main bass library, open MP3, MP2, MP1, OGG, WAV, AIFF, MOD. Link(1) for all bass*
bass_aac.dll    2.4.5.1  01/04/2015  Optional to read AAC/MA4/MP4.
bass_ac3.dll    2.4.0.5  28/08/2015  Optional to read AC3.
bass_adx.dll    2.4.1.1  05/11/2009  Optional to read CRI adx.
bass_aix.dll    2.4.1.1  24/11/2009  Optional to read CRI aix. (not tested)
bass_alac.dll   2.4.0.0  02/02/2016  Optional to read Alac, Apple Lossless Audio.
bass_ape.dll    2.4.2.0  25/11/2014  Optional to read APE Monkey audio.
bass_cda.dll    2.4.6.0  11/06/2014  Optional to read CD Audio.  (2)
bass_dsd.dll    2.4.0.0  17/12/2014  Optional to read Direct Stream Digital (dff, dsf).
bass_flac.dll   2.4.2.3  17/05/2016  Optional to read Flac.
bass_midi.dll   2.4.9.0  04/12/2014  Optional to read MIDI Audio (3).
bass_mpc.dll    2.4.1.2  17/07/2015  Optional to read MPC, MusePack.
bass_ofr.dll    2.4.0.2  24/04/2009  Optional to read OFR, Require OptimFROG.dll (4).
bass_opus.dll   2.4.1.8  04/08/2016  Optional to read Opus.
bass_spx.dll    2.4.3.2  07/08/2015  Optional to read SPX, Speex.
bass_tta.dll    2.4.0.2  05/02/2016  Optional to read TTA, True Audio encoder.
bass_wma.dll    2.4.5.1  04/04/2016  Optional to read WMA, Windows Media Audio.
bass_wv.dll     2.4.5.1  27/03/2015  Optional to read WV, WavPack Audio.

ffms2.dll       2.23 RC  10/10/2016 to decode many audio even in container. https://github.com/FFMS/ffms2/releases. Require AviSinth 2.6.0, with 2.5.8 use LSMASHSource.dll

LSMASHSource.dll  (5)    30/09/2016 to decode many audio even in container. http://avisynth.nl/index.php/LSMASHSource. Require vcruntime140.dll

NicAudio.dll	2.0.6    27/08/2012 To decode AC3, DTS, MPEG, LPCM, and other uncompressed formats (included)

NOTES:

(1) The oficial Bass* releases v2.4 are in http://www.un4seen.com/bass.html. Rename some of them to the name here.

(2) A .cda file is how you see tracks from CD Audio in the File System. Just load the TrackXX.cda to rip.

(3) To convert .mid or .kar you need a SoundFont in c:\windows\system32 (or c:\windows\sysWOW64 in 64 bit OS). See Others\MIDI_Readme.txt

(4) To decode .ofr file, you need OptimFROG.dll in BeHappy main folder (suplied the last version from 5.100)

(5) All formats supported by Bass, except the precedent ones (.cda, .mid, .kar and .ofr), are supported by LSMASHSource.dll.
Try always first LWlibavAudioSource (or LSMASHAudioSource for mp4,mov,3gp containers).
Also support DTS-HD, TrueHD, E-AC3 and many others.
