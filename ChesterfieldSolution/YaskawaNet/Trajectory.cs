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

        #region Linear trajectories

        /// <summary>
        /// The x axis for the vector list position.
        /// </summary>
        public Vector<double> X { get; set; }

        /// <summary>
        /// The y axis for the vector list  position.
        /// </summary>
        public Vector<double> Y { get; set; }

        /// <summary>
        /// The z axis for the vector list position.
        /// </summary>
        public Vector<double> Z { get; set; }
        #endregion

        #region Rotation trajectories
        /// <summary>
        /// The x rotation axis for the vector list  position.
        /// </summary>
        public Vector<double> Rx { get; set; }

        /// <summary>
        /// The y rotation axis for the vector list  position.
        /// </summary>
        public Vector<double> Ry { get; set; }

        /// <summary>
        /// The z rotation axis for thevector list position.
        /// </summary>
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

        public Trajectory()
        {
        }
        public void InsertOriginPlace(bool forward = true)
        {

            RobotPosition originPoint = new RobotPosition() { X = 0, Y = 0, Z = 0, Rx = 0, Ry = 0, Rz = 0 };

            //todo:decide if to return new one or the input one (chenged).
            if (!forward)
            {
                this.Add(originPoint);
            }
            else
            {
                this.Insert(0, originPoint);
            }
        }
        public int IndexOf(RobotPosition item)
        {
            throw new NotImplementedException();
        }
        public void Insert(int index, RobotPosition item)
        {
            List<double> x = X.ToList();
            List<double> y = Y.ToList();
            List<double> z = Z.ToList();
            List<double> rx = Rx.ToList();
            List<double> ry = Ry.ToList();
            List<double> rz = Rz.ToList();
            x.Insert(index, item.X);
            y.Insert(index, item.Y);
            z.Insert(index, item.Z);
            rx.Insert(index, item.Rx);
            ry.Insert(index, item.Ry);
            rz.Insert(index, item.Rz);

            X = Vector<double>.Build.Dense(x.ToArray());
            Y = Vector<double>.Build.Dense(y.ToArray());
            Z = Vector<double>.Build.Dense(z.ToArray());
            Rx = Vector<double>.Build.Dense(rx.ToArray());
            Ry = Vector<double>.Build.Dense(ry.ToArray());
            Rz = Vector<double>.Build.Dense(rz.ToArray());
        }
        public void RemoveAt(int index)
        {
            List<double> x = X.ToList();
            List<double> y = Y.ToList();
            List<double> z = Z.ToList();
            List<double> rx = Rx.ToList();
            List<double> ry = Ry.ToList();
            List<double> rz = Rz.ToList();
            x.RemoveAt(index);
            y.RemoveAt(index);
            z.RemoveAt(index);
            rx.RemoveAt(index);
            ry.RemoveAt(index);
            rz.RemoveAt(index);

            X = Vector<double>.Build.Dense(x.ToArray());
            Y = Vector<double>.Build.Dense(y.ToArray());
            Z = Vector<double>.Build.Dense(z.ToArray());
            Rx = Vector<double>.Build.Dense(rx.ToArray());
            Ry = Vector<double>.Build.Dense(ry.ToArray());
            Rz = Vector<double>.Build.Dense(rz.ToArray());
        }
        public void Add(RobotPosition item)
        {
            if (this.Count == 0)
            {
                X = Vector<double>.Build.Dense(1);
                Y = Vector<double>.Build.Dense(1);
                Z = Vector<double>.Build.Dense(1);
                Rx = Vector<double>.Build.Dense(1);
                Ry = Vector<double>.Build.Dense(1);
                Rz = Vector<double>.Build.Dense(1);

                X[0] = item.X;
                Y[0] = item.Y;
                Z[0] = item.Z;
                Rx[0] = item.Rx;
                Ry[0] = item.Ry;
                Rz[0] = item.Rz;
            }
            else
            {
                this.Insert(this.Count, item);
            }
        }
        public void Clear()
        {
            X.Clear();
            Y.Clear();
            Z.Clear();
            Rx.Clear();
            Ry.Clear();
            Rz.Clear();
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
            inverseTrajectory.X = Vector<double>.Build.Dense(length);
            inverseTrajectory.Y = Vector<double>.Build.Dense(length);
            inverseTrajectory.Z = Vector<double>.Build.Dense(length);
            inverseTrajectory.Rx = Vector<double>.Build.Dense(length);
            inverseTrajectory.Ry = Vector<double>.Build.Dense(length);
            inverseTrajectory.Rz = Vector<double>.Build.Dense(length);

            //inverse the original trajectory into the new trajectory.
            for (int i = 0; i < length; i++)
            {
                int index = length - 1 - i;

                inverseTrajectory.X[i] = this.X[index];
                inverseTrajectory.Y[i] = this.Y[index];
                inverseTrajectory.Z[i] = this.Z[index];
                inverseTrajectory.Rx[i] = this.Rx[index];
                inverseTrajectory.Ry[i] = this.Ry[index];
                inverseTrajectory.Rz[i] = this.Rz[index];
            }

            //return the inversed trajectory.
            return inverseTrajectory;
        }
        public Trajectory Clone()
        {
            Trajectory clonedTrajectory = new Trajectory();

            for (int i = 0; i < this.Count; i++)
            {
                clonedTrajectory.Add(this[i]);
            }

            return clonedTrajectory;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
