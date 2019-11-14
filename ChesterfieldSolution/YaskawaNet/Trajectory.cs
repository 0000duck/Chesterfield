using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaskawaNet
{
    public class Trajectory : IList<RobotPosition>, ICloneable
    {
        #region Fields

        #region Joints trajectories

        public Vector<double> S { get; set; }
        public Vector<double> L { get; set; }
        public Vector<double> U { get; set; }
        public Vector<double> R { get; set; }
        public Vector<double> B { get; set; }
        public Vector<double> T { get; set; }
        public Vector<double> EX7Pulse { get; set; }
        public Vector<double> EX7Mm { get; set; }
        #endregion

        #region Linear trajectories

        public Vector<double> X { get; set; }
        public Vector<double> Y { get; set; }
        public Vector<double> Z { get; set; }
        #endregion

        #region Rotation trajectories
        public Vector<double> Rx { get; set; }
        public Vector<double> Ry { get; set; }
        public Vector<double> Rz { get; set; }

        public int Count => (X != null) ? X.Count : 0;

        public object SyncRoot
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsSynchronized
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsFixedSize
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        RobotPosition IList<RobotPosition>.this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public RobotPosition this[int index]
        {
            get
            {
                return new RobotPosition()
                {
                    X = X[index],
                    Y = Y[index],
                    Z = Z[index],
                    Rx = Rx[index],
                    Ry = Ry[index],
                    Rz = Rz[index],
                };
            }
            set
            {
                X[index] = value.X;
                Y[index] = value.Y;
                Z[index] = value.Z;
                Rx[index] = value.Rx;
                Ry[index] = value.Ry;
                Rz[index] = value.Rz;
            }
        }
        #endregion

        #endregion

        #region Methods

        public Trajectory()
        {
        }
        public void InsertOriginPlace(bool forward = true)
        {
            RobotPosition originPoint = new RobotPosition() { X = 0, Y = 0, Z = 0, Rx = 0, Ry = 0, Rz = 0, SAxis = 0, LAxis = 0, UAxis = 0, RAxis = 0, BAxis = 0, TAxis = 0, E7Axis = 0 };
            try
            {
                #region
                //todo:decide if to return new one or the input one (chenged).
                if (!forward)
                {
                    this.Add(originPoint);
                }
                else
                {
                    this.Insert(0, originPoint);
                }
                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        public int IndexOf(RobotPosition item)
        {
            throw new NotImplementedException();
        }
        public void Insert(int index, RobotPosition item)
        {
            List<double> s = S.ToList();
            List<double> l = L.ToList();
            List<double> u = U.ToList();
            List<double> r = R.ToList();
            List<double> b = B.ToList();
            List<double> t = T.ToList();
            List<double> ex7Pulse = EX7Pulse.ToList();

            List<double> x = X.ToList();
            List<double> y = Y.ToList();
            List<double> z = Z.ToList();
            List<double> rx = Rx.ToList();
            List<double> ry = Ry.ToList();
            List<double> rz = Rz.ToList();
            List<double> ex7Mm = EX7Mm.ToList();

            try
            {
                #region
                x.Insert(index, item.X);
                y.Insert(index, item.Y);
                z.Insert(index, item.Z);
                rx.Insert(index, item.Rx);
                ry.Insert(index, item.Ry);
                rz.Insert(index, item.Rz);
                ex7Mm.Insert(index, item.E7Axis);

                s.Insert(index, item.RobotPulsePositions[0]);
                l.Insert(index, item.RobotPulsePositions[1]);
                u.Insert(index, item.RobotPulsePositions[2]);
                r.Insert(index, item.RobotPulsePositions[3]);
                b.Insert(index, item.RobotPulsePositions[4]);
                t.Insert(index, item.RobotPulsePositions[5]);
                ex7Pulse.Insert(index, item.RobotPulsePositions[6]);

                X = Vector<double>.Build.Dense(x.ToArray());
                Y = Vector<double>.Build.Dense(y.ToArray());
                Z = Vector<double>.Build.Dense(z.ToArray());
                Rx = Vector<double>.Build.Dense(rx.ToArray());
                Ry = Vector<double>.Build.Dense(ry.ToArray());
                Rz = Vector<double>.Build.Dense(rz.ToArray());
                EX7Mm = Vector<double>.Build.Dense(ex7Mm.ToArray());

                S = Vector<double>.Build.Dense(s.ToArray());
                L = Vector<double>.Build.Dense(l.ToArray());
                U = Vector<double>.Build.Dense(u.ToArray());
                R = Vector<double>.Build.Dense(r.ToArray());
                B = Vector<double>.Build.Dense(b.ToArray());
                T = Vector<double>.Build.Dense(t.ToArray());
                EX7Pulse = Vector<double>.Build.Dense(ex7Pulse.ToArray());
                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        public void RemoveAt(int index)
        {
            List<double> s = S.ToList();
            List<double> l = L.ToList();
            List<double> u = U.ToList();
            List<double> r = R.ToList();
            List<double> b = B.ToList();
            List<double> t = T.ToList();
            List<double> ex7Pulse = EX7Pulse.ToList();

            List<double> x = X.ToList();
            List<double> y = Y.ToList();
            List<double> z = Z.ToList();
            List<double> rx = Rx.ToList();
            List<double> ry = Ry.ToList();
            List<double> rz = Rz.ToList();
            List<double> ex7Mm = EX7Mm.ToList();

            try
            {
                #region
                s.RemoveAt(index);
                l.RemoveAt(index);
                u.RemoveAt(index);
                r.RemoveAt(index);
                b.RemoveAt(index);
                t.RemoveAt(index);
                ex7Pulse.RemoveAt(index);

                x.RemoveAt(index);
                y.RemoveAt(index);
                z.RemoveAt(index);
                rx.RemoveAt(index);
                ry.RemoveAt(index);
                rz.RemoveAt(index);
                ex7Mm.RemoveAt(index);

                S = Vector<double>.Build.Dense(s.ToArray());
                L = Vector<double>.Build.Dense(l.ToArray());
                U = Vector<double>.Build.Dense(u.ToArray());
                R = Vector<double>.Build.Dense(r.ToArray());
                B = Vector<double>.Build.Dense(b.ToArray());
                T = Vector<double>.Build.Dense(t.ToArray());
                EX7Pulse = Vector<double>.Build.Dense(ex7Pulse.ToArray());

                X = Vector<double>.Build.Dense(x.ToArray());
                Y = Vector<double>.Build.Dense(y.ToArray());
                Z = Vector<double>.Build.Dense(z.ToArray());
                Rx = Vector<double>.Build.Dense(rx.ToArray());
                Ry = Vector<double>.Build.Dense(ry.ToArray());
                Rz = Vector<double>.Build.Dense(rz.ToArray());
                EX7Mm = Vector<double>.Build.Dense(ex7Mm.ToArray());
                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        public void Add(RobotPosition item)
        {
            try
            {
                #region
                if (this.Count == 0)
                {
                    #region
                    X = Vector<double>.Build.Dense(1);
                    Y = Vector<double>.Build.Dense(1);
                    Z = Vector<double>.Build.Dense(1);
                    Rx = Vector<double>.Build.Dense(1);
                    Ry = Vector<double>.Build.Dense(1);
                    Rz = Vector<double>.Build.Dense(1);

                    S = Vector<double>.Build.Dense(1);
                    L = Vector<double>.Build.Dense(1);
                    U = Vector<double>.Build.Dense(1);
                    R = Vector<double>.Build.Dense(1);
                    B = Vector<double>.Build.Dense(1);
                    T = Vector<double>.Build.Dense(1);

                    EX7Pulse = Vector<double>.Build.Dense(1);
                    EX7Mm = Vector<double>.Build.Dense(1);

                    X[0] = item.X;
                    Y[0] = item.Y;
                    Z[0] = item.Z;
                    Rx[0] = item.Rx;
                    Ry[0] = item.Ry;
                    Rz[0] = item.Rz;
                    EX7Mm[0] = item.E7Axis;

                    S[0] = item.RobotPulsePositions[0];
                    L[0] = item.RobotPulsePositions[1];
                    U[0] = item.RobotPulsePositions[2];
                    R[0] = item.RobotPulsePositions[3];
                    B[0] = item.RobotPulsePositions[4];
                    T[0] = item.RobotPulsePositions[5];

                    EX7Pulse[0] = item.RobotPulsePositions[6];
                    #endregion
                }
                else
                {
                    this.Insert(this.Count, item);
                }
                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        public void Clear()
        {
            try
            {
                #region
                S.Clear();
                L.Clear();
                U.Clear();
                R.Clear();
                B.Clear();
                T.Clear();
                EX7Pulse.Clear();

                X.Clear();
                Y.Clear();
                Z.Clear();
                Rx.Clear();
                Ry.Clear();
                Rz.Clear();
                EX7Mm.Clear();
                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }
        }
        public bool Contains(RobotPosition item)
        {
            throw new NotImplementedException();
        }
        public void CopyTo(RobotPosition[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public bool Remove(RobotPosition item)
        {
            throw new NotImplementedException();
        }
        public IEnumerator<RobotPosition> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get a new Trajectory (independent from current) reversed to the current Trajectory.
        /// </summary>
        /// <returns>The new  reversed Trajectory.</returns>
        public Trajectory ToReverse()
        {
            //the inversed trajectory to be returned.
            Trajectory inverseTrajectory = new Trajectory();
            //initialization for the the trajectorry yo be retund.
            int length = this.Count;

            try
            {
                #region
                inverseTrajectory.S = Vector<double>.Build.Dense(length);
                inverseTrajectory.L = Vector<double>.Build.Dense(length);
                inverseTrajectory.U = Vector<double>.Build.Dense(length);
                inverseTrajectory.R = Vector<double>.Build.Dense(length);
                inverseTrajectory.B = Vector<double>.Build.Dense(length);
                inverseTrajectory.T = Vector<double>.Build.Dense(length);
                inverseTrajectory.EX7Pulse = Vector<double>.Build.Dense(length);

                inverseTrajectory.X = Vector<double>.Build.Dense(length);
                inverseTrajectory.Y = Vector<double>.Build.Dense(length);
                inverseTrajectory.Z = Vector<double>.Build.Dense(length);
                inverseTrajectory.Rx = Vector<double>.Build.Dense(length);
                inverseTrajectory.Ry = Vector<double>.Build.Dense(length);
                inverseTrajectory.Rz = Vector<double>.Build.Dense(length);
                inverseTrajectory.EX7Mm = Vector<double>.Build.Dense(length);

                //inverse the original trajectory into the new trajectory.
                for (int i = 0; i < length; i++)
                {
                    #region
                    int index = length - 1 - i;

                    inverseTrajectory.S[i] = this.S[index];
                    inverseTrajectory.L[i] = this.L[index];
                    inverseTrajectory.U[i] = this.U[index];
                    inverseTrajectory.R[i] = this.R[index];
                    inverseTrajectory.B[i] = this.B[index];
                    inverseTrajectory.T[i] = this.T[index];
                    inverseTrajectory.EX7Pulse[i] = this.EX7Pulse[index];

                    inverseTrajectory.X[i] = this.X[index];
                    inverseTrajectory.Y[i] = this.Y[index];
                    inverseTrajectory.Z[i] = this.Z[index];
                    inverseTrajectory.Rx[i] = this.Rx[index];
                    inverseTrajectory.Ry[i] = this.Ry[index];
                    inverseTrajectory.Rz[i] = this.Rz[index];
                    inverseTrajectory.EX7Mm[i] = this.EX7Mm[index];
                    #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            //return the inversed trajectory.
            return inverseTrajectory;
        }
        public Trajectory Clone()
        {
            Trajectory clonedTrajectory = new Trajectory();

            try
            {
                for (int i = 0; i < this.Count; i++)
                {
                    #region
                    clonedTrajectory.Add(this[i]);

                    clonedTrajectory.S[i] = this.S[i];
                    clonedTrajectory.L[i] = this.L[i];
                    clonedTrajectory.U[i] = this.U[i];
                    clonedTrajectory.R[i] = this.R[i];
                    clonedTrajectory.B[i] = this.B[i];
                    clonedTrajectory.T[i] = this.T[i];
                    clonedTrajectory.EX7Pulse[i] = this.EX7Pulse[i];

                    clonedTrajectory.X[i] = this.X[i];
                    clonedTrajectory.Y[i] = this.Y[i];
                    clonedTrajectory.Z[i] = this.Z[i];
                    clonedTrajectory.Rx[i] = this.Rx[i];
                    clonedTrajectory.Ry[i] = this.Ry[i];
                    clonedTrajectory.Rz[i] = this.Rz[i];
                    clonedTrajectory.EX7Mm[i] = this.EX7Mm[i];
                    #endregion
                }
            }
            catch (Exception ex)
            {
                DiagnosticException.ExceptionHandler(ex);
            }

            return clonedTrajectory;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion
    }
}
