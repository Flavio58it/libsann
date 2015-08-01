using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
 * Wrapper dll of libsann C++ library
 * Matteo Tosato - 2014
 */

namespace libsannNET
{
    #region [ Enum ]

    public enum ActivationNeuroMode : int
    {
        SIGMOIDAL = 0,
        HYPERBOLIC_TANGENT = 1,
        STEP = 2,
        IDENTITY = 3
    };

    public enum SynInitMode : int
    {
        POSITIVE_NEGATIVE_RANGE = 0,
        POSITIVE_RANGE = 1,
        NGUYEN_WINDROW = 2,
        FAN_IN = 3,
        ZERO = 4,
        NONE = 5
    };

    #endregion

    public class LibsannInvoke
    {
        #region [ Pinvoke ]

        [DllImport("libsann.dll")]
        public extern static void libsannInit();

        #region [ Polymorphic functions ]

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void NetSetInput(
            IntPtr net,
            [MarshalAs(UnmanagedType.LPArray)] double[] pattern,
            int size);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr NetExec(IntPtr net, uint outSize);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static uint GetEpoch(IntPtr net);

        #endregion

        #region [ MLP ]

        #region [ Elman Mlp ]

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr EMlpInstantiate(
            uint numberOfInputNeurons,
            uint numberOfHiddenLayers,
            [MarshalAs(UnmanagedType.LPArray)] uint[] numberOfUnitsForHiddenLayers,
            uint numberOfUnitsOfOutputLayer,
            bool useBias,
            ActivationNeuroMode mode);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void EMlpDelete(IntPtr net);

        #endregion

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr MlpGetWeights(IntPtr net);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static uint MlpGetWeightsNumber(IntPtr net);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr MlpLoadWeights(
            IntPtr net,
            [MarshalAs(UnmanagedType.LPArray)] double[] weights,
            int num);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr MlpInstantiate(
            uint numberOfInputNeurons,
            uint numberOfHiddenLayers,
            [MarshalAs(UnmanagedType.LPArray)] uint[] numberOfUnitsForHiddenLayers,
            uint numberOfUnitsOfOutputLayer,
            bool useBias,
            ActivationNeuroMode mode);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr InstantiateBackPropagationTraininglgorithm(
            IntPtr net,
            double errorTarget,
            uint maxEpochs,
            double learningRate,
            double beta);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr InstantiateResilientPropagationTrainingAlgorithm(
            IntPtr net,
            double errorTarget,
            uint maxEpochs,
            double maxUpdateValue,
            double minUpdateValue,
            double growthFactor,
            double decreaseFactor);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static double MlpTrainingWithPropagation(
            IntPtr net,
            IntPtr inputPatterns,
            IntPtr outputPatterns,
            uint inputRows,
            uint numberOfInputUnits,
            uint numberOfUnitsForOutputLayer,
            IntPtr trainAlg,
            SynInitMode mode);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void MlpDelete(IntPtr net);

        #endregion

        #region [ Stats utility ]

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr BuildStatsListener(IntPtr trainingPropAlg);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void DestroyPropStatsListener(IntPtr trainingPropAlg);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetPropOutputValues(IntPtr statsListener, out int _out_patterns_count);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetPropErrorValue(IntPtr statsListener, out int _out_size);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void ResetPropStats(IntPtr statsListener);

        #endregion

        #region [ M-Adeline ]

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr mAdelineInstantiate(
            uint numberOfInputNeurons,
            uint numberOfOutputNeurons,
            bool useBias,
            ActivationNeuroMode mode);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static double mAdelineTrainingWithWindrowHoff(
            IntPtr net,
            IntPtr inputPatterns,
            IntPtr outputPatterns,
            uint inputRows,
            uint numberOfInputNeurons,
            uint numberOfOutputNeurons,
            double errorTarget,
            uint maxEpochs,
            double learningRate,
            SynInitMode mode);

        [DllImport("libsann.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void mAdelineDelete(IntPtr net);

        #endregion

        #endregion

        #region [ Utils ]

        /// <summary>
        /// Conversion to managed matrix array to unmanaged matrix
        /// </summary>
        /// <param name="InMatrix">Managed matrix 2D</param>
        /// <param name="OutMatrix">Unmanaged matrix 2D - out</param>
        public static void MatrixConversion(double[][] InMatrix, out IntPtr OutMatrix)
        {
            int rows = InMatrix.Count();
            int cols = InMatrix[0].Count();

            IntPtr[] buildArray = new IntPtr[rows];
            for(int i = 0; i < rows; i++)
            {
                buildArray[i] = Marshal.AllocCoTaskMem(Marshal.SizeOf(InMatrix[i][0]) * cols);
                Marshal.Copy(InMatrix[i], 0, buildArray[i], cols);
            }
            OutMatrix = Marshal.AllocCoTaskMem(Marshal.SizeOf(buildArray[0]) * rows);
            Marshal.Copy(buildArray, 0, OutMatrix, rows);
        }

        /// <summary>
        /// Conversion to unmanaged matrix to managed managed
        /// </summary>
        /// <param name="InMatrix">Unmanaged 2D matrix</param>
        /// <param name="rows">Number of rows</param>
        /// <param name="cols">Number of cols</param>
        /// <param name="OutMatrix">Managed matrix</param>
        public static void MatrixConversion(IntPtr InMatrix, int rows, int cols, out double[][] OutMatrix)
        {
            OutMatrix = new double[rows][];
            for(int i = 0; i < rows; i++)
                OutMatrix[i] = new double[cols];

            IntPtr[] IntPtrRows = new IntPtr[rows];
            Marshal.Copy(InMatrix, IntPtrRows, 0, rows);
            
            for(int r = 0; r < rows ; r++)
            {
                Marshal.Copy(IntPtrRows[r], OutMatrix[r], 0, cols);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InMatrix"></param>
        /// <param name="len"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="OutMatrix"></param>
        public static void MatrixConversion(IntPtr InMatrix, int len, int rows, int cols, out List<double[][]> OutMatrix)
        {
            double[][][] M = new double[len][][];
            for(int i = 0; i < len; i++)
                M[i] = new double[rows][];
                
            IntPtr[] IntPtrLen = new IntPtr[len];
            Marshal.Copy(InMatrix, IntPtrLen,0, len);

            for(int i = 0; i < len; i++)
            {
                IntPtr[] IntPtrRows = new IntPtr[rows];
                Marshal.Copy(IntPtrLen[i], IntPtrRows, 0, rows);

                for(int j = 0; j < rows; j++)
                {
                    M[i][j] = new double[cols];
                    Marshal.Copy(IntPtrRows[j], M[i][j], 0, cols);
                }
            }

            OutMatrix = new List<double[][]>(M);
        }

        #endregion
    }
}