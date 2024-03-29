# Ateliware Tech Assessment

Repository to develop the engineering tech assessment for Ateliware Fullstack Dev job

## Context

Géfersom Bejos, CEO of the large Brazilian e-commerce company amazônia, hired ateliware to develop a new system responsible for calculating the fastest delivery route for his new drone system.

Once the starting point, the object collection point, and the delivery point are defined, his drones should, in the shortest time possible, reach their destination.

Amazon is already a technology company, so it will be providing a service by which it is possible to find out the time needed to move between segments.

## The Challenge

The challenge is based on building, within 1 week, **a modern web application, in any development technology** (Python/Django, express.js + React, PHP/Laravel, anything!) that allows the user to inform the drone's origin position, the object pickup position and the delivery destination.

All these positions should be informed as if they were coordinates of a chessboard by letters on the horizontal axis (A to H) and numbers on the vertical axis (1 to 8)

Once these 3 data are entered, the application must **access an API** that, through a JSON, will inform the **time required to move between all the immediate vertical and horizontal coordinates**, for example from A1 to A2 and B1, or from C3 to C4, B3, C2 and D3.

**API's endpoint:** https://mocki.io/v1/10404696-fd43-4481-a7ed-f9369073252f

JSON format example:

```
{
    "A1": {
        "B1": 13.42
        "A2": 23.55
    },
    "B1": { [...] },
    [...]
}
```

This means that the drone at position A1 will take 13.42 seconds to move to coordinate B1 and 23.55 seconds to coordinate A2.

After hitting the API, and given the coordinates entered, the application should calculate and inform on the screen the **fastest path to pick up the package and deliver it to the destination, as well as the time elapsed by this path**.

The movements between the coordinates of the trajectory will only occur vertically (x-axis, of numbers) or horizontally (y-axis, of letters).

The screen should **also list the last 10 trips previously calculated** by the application.

The assessment object of delivery needs to be a **public repository on GitHub** with the code, and a **link for cloud remote access** to the working application.

## The Solution

The solution can be acessed at the following url [Ateliware Assessment](https://ateliwareassessment.web.app/). It was developed using Unity version 2021.3.15f1 and deployed using Firebase. 

The application was developed to be used on a computer and don't work correctly on mobile devices.

### How to Use

The application uses both keyboard and mouse, but can be used with keyboard only. It's possible to select each input field with the mouse and insert the desired value, finishing with a click on the ```Get Route``` button, or you can use the ```Tab``` key to transition between the fields and finish the operation with the ```Return``` key. If the inputs are valid, the shortest route will be calculated and a animation of the path will be displayed. The animation can be skipped with the ```Space``` key or by clicking on the ```Skip``` button.

![Running application](/Images/running.png)

### The Software

The software consists of three main areas: user input, list of deliveries and print of the path. The user input area receives the 3 points that defines the start point of the drone, the location of the delivery and the destination. After clicking the "Get Route" button, the informations of the delivery are added to the list of last deliveries and printed at the separated area. At the same time the path made by the algorithm are inserted at the botton of the page and a animation of the drone making the route are presented. The duration of the animation varies according to the time needed to traverse the path, with the passage between one node and the other taking a twentieth of the time in seconds described on the graph.

The Single Source Shortest Path Dijkstra algorithm was used to define the route, using a adjacency list to represent the board. A list of "parents" was added to enable a backing track to get the path made.

![Drone animation](/Images/pathAnimation.png)

### References Links

- Dijkstra Algorithm
    - https://github.com/MatheusFaria/TEP/blob/master/Grafos/sssp.md
    - https://github.com/edsomjr/TEP/blob/master/Grafos/slides/dijkstra/dijkstra.pdf
- Unity Test Framework
    - https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/index.html
    - https://www.youtube.com/watch?v=pr5FBtu5SvQ&ab_channel=MinaP%C3%AAcheux
    - https://github.com/MinaPecheux/UnityTutorials-UnitTesting/blob/master/Assets/EditorTests/MatrixGettersTests.cs
- Deploy WebGL
    - https://forum.unity.com/threads/best-cloud-server-data-store-for-uploading-webgl-games.433915/
    - https://medium.com/@aboutin/host-unity-games-on-github-pages-for-free-2ed6b4d9c324
    - https://levelup.gitconnected.com/how-to-publish-your-unity3d-html5-application-or-game-to-aws-3bb053b59d21
    - https://medium.com/firebase-developers/firebase-with-unity-even-in-webgl-build-8891e6f9b33c
