# Dynamic-Gesture-Recognition-in-3D-space
21 Class Dynamic Gesture Recognition in 3D space(Leap Motion, VR, PC)

## Screenshot([video link1](https://youtu.be/gDamQfYpSVw), [video link2](https://youtu.be/Xl6bw05PeW4))
![capture](https://user-images.githubusercontent.com/61224394/109899673-8ec5b080-7cd9-11eb-9e9b-0fe4d02dd13b.gif)

## Features
1. Dynamic Gesture Recognition in 3D space
2. Supported 21 Gesture Categories 
3. Model Serving using ML Agent
4. [Unity3D Demo](https://github.com/jhcnode/Dynamic-Gesture-Recognition-in-3D-space/releases/download/1.0/Release.zip)

## Information 
1. Collected gesture data using leap motion
2. With main pivot(etc..head direction,front direction), you can use gestures in 3D space.
3. The demo requires a leap motion

## Setup
Tensorflow(==1.4.0)  
Unity3D(>=2018)  

## Model Train & Export(Freeze)

### [Train](https://github.com/jhcnode/Dynamic-Gesture-Recognition-in-3D-space/blob/main/python/MLP%2BSelu%2B5%20Hidden%20Layer.ipynb)
  
### Export(Freeze)
1. [Epxort a model](https://github.com/jhcnode/Dynamic-Gesture-Recognition-in-3D-space/blob/main/python/model_trainable(custom_only_model_save).ipynb)  
2. [Create *.pb file](https://github.com/jhcnode/Dynamic-Gesture-Recognition-in-3D-space/blob/main/python/freeze_graph.ipynb)  

## Reference
1. [Chae, Ji Hun, et al. "Deep Learning Based 3D Gesture Recognition Using Spatio-Temporal Normalization." Journal of Korea Multimedia Society 21.5 (2018): 626-637.](https://www.koreascience.or.kr/article/JAKO201818564288222.page)
2. [Wobbrock, Jacob O., Andrew D. Wilson, and Yang Li. "Gestures without libraries, toolkits or training: a $1 recognizer for user interface prototypes." Proceedings of the 20th annual ACM symposium on User interface software and technology. 2007.](https://dl.acm.org/doi/10.1145/1294211.1294238)
