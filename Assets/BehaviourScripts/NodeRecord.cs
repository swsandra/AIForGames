using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public struct NodeRecord{

        public Node node;
        public Connection connection;
        public float costSoFar;
        public float estimatedTotalCost;

        public NodeRecord(Node node, Connection connection, float costSoFar, float estimatedTotalCost){
            this.node=node;
            this.connection=connection;
            this.costSoFar=costSoFar;
            this.estimatedTotalCost=estimatedTotalCost;
        }

    }