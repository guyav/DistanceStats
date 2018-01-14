# DistanceStats
This program uses Machine Learning methods to sort settlements in Israel by their remoteness. However, remoteness is not well defined, so several methods are used for this goal – each with different results.

## Methods
Currently, the program supports the folllowing methods:
* **Simple Distance Calculation:** The program finds the closest place to every place. The place that is the most distant from its nearest place is considered the most remote.
* **Advanced Distance Calculation:** The program checks the distance of each place to a given set of "anchor places". The "anchor places" are relatively central places in Israel. The place that is the most distant from its nearest "anchor city" is considered the most remote.
* **[Agglomerative Hierarchical Clustering (AGNES)](https://en.wikipedia.org/wiki/Hierarchical_clustering):** The program clusters the places. The first two places to be joined to create a cluster, are considered as the most non-remote places. The last place to be joined to a cluster is considered to be the most remote place. The distance between clusters can be counted with one of four metrics:
  * **[Single Link](https://en.wikipedia.org/wiki/Single-linkage_clustering)**: The distance between cluster *A* and cluster *B* is defined as the **shortest** distance of any place in cluster *A* to any place in cluster *B*. This is actually identical to the "Simple Distance Calculation" method.
  * **[Complete Link](https://en.wikipedia.org/wiki/Complete-linkage_clustering):** The distance between cluster *A* and cluster *B* is defined as the **longest** distance of any place in cluster *A* to any place in cluster *B*.
  * **[Average Link (UPGMA)](https://en.wikipedia.org/wiki/UPGMA):** The distance between the clusters is defined as the average of all the distances between all pairs of places in each cluster.
  * **Centroid To Centroid:** The distance between the clusters is defined as the distance between the centroids of each cluster.

## Requirements
You will need a PBF file of Israel for the program to work. A suitable file can be obtained here: https://download.geofabrik.de/asia/israel-and-palestine.html. By default, the program searches for this file on the desktop of the current user.
