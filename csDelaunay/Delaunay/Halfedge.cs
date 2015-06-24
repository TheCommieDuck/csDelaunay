﻿using System.Collections;
using System.Collections.Generic;

namespace csDelaunay {

	public class Halfedge {

		#region Pool
		private static Queue<Halfedge> pool = new Queue<Halfedge>();

		public static Halfedge Create(Edge edge, byte lr) {
			if (pool.Count > 0) {
				return pool.Dequeue().Init(edge,lr);
			} else {
				return new Halfedge(edge,lr);
			}
		}
		public static Halfedge CreateDummy() {
			return Create(null, 1);
		}
		#endregion

		#region Object
		public Halfedge edgeListLeftNeighbor;
		public Halfedge edgeListRightNeighbor;
		public Halfedge nextInPriorityQueue;

		public Edge edge;
		public byte leftRight;
		public Vertex vertex;

		// The vertex's y-coordinate in the transformed Voronoi space V
		public float ystar;

		public Halfedge(Edge edge, byte lr) {
			Init(edge, lr);
		}

		private Halfedge Init(Edge edge, byte lr) {
			this.edge = edge;
			leftRight = lr;
			nextInPriorityQueue = null;
			vertex = null;

			return this;
		}

		public override string ToString() {
			return "Halfedge (LeftRight: " + leftRight + "; vertex: " + vertex + ")";
		}

		public void Dispose() {
			if (edgeListLeftNeighbor != null || edgeListRightNeighbor != null) {
				// still in EdgeList
				return;
			}
			if (nextInPriorityQueue != null) {
				// still in PriorityQueue
				return;
			}
			edge = null;
			leftRight = 0;
			vertex = null;
			pool.Enqueue(this);
		}

		public void ReallyDispose() {
			edgeListLeftNeighbor = null;
			edgeListRightNeighbor = null;
			nextInPriorityQueue = null;
			edge = null;
			leftRight = 0;
			vertex = null;
			pool.Enqueue(this);
		}

		public bool IsLeftOf(Vector2f p) {
			Site topSite;
			bool rightOfSite, above, fast;
			float dxp, dyp, dxs, t1, t2, t3, y1;

			topSite = edge.RightSite;
			rightOfSite = p.X > topSite.x;
			if (rightOfSite && this.leftRight == LR.Left) {
				return true;
			}
			if (!rightOfSite && this.leftRight == LR.Right) {
				return false;
			}

			if (edge.a == 1) {
				dyp = p.Y - topSite.y;
				dxp = p.X - topSite.x;
				fast = false;
				if ((!rightOfSite && edge.b < 0) || (rightOfSite && edge.b >= 0)) {
					above = dyp >= edge.b * dxp;
					fast = above;
				} else {
					above = p.X + p.Y * edge.b > edge.c;
					if (edge.b < 0) {
						above = !above;
					} 
					if (!above) {
						fast = true;
					}
				}
				if (!fast) {
					dxs = topSite.x - edge.LeftSite.x;
					above = edge.b * (dxp * dxp - dyp * dyp) < dxs * dyp * (1+2 * dxp/dxs + edge.b * edge.b);
					if (edge.b < 0) {
						above = !above;
					}
				}
			} else {
				y1 = edge.c - edge.a * p.X;
				t1 = p.Y - y1;
				t2 = p.X - topSite.x;
				t3 = y1 - topSite.y;
				above = t1 * t1 > t2 * t2 + t3 * t3;
			}
			return this.leftRight == LR.Left ? above : !above;
		}
		#endregion
	}
}
