#################
#Low Poly Water Shader
#Author: Botzenhardt3d@gmail.com
#01/07/2018
#################

For the best results use the following.

Basic Setup-
Apply LowPolyWaterShader script to a new material.
Assign any maps needed in the new material.
Apply LowPolyWaterVertexNoise script to desired water surface.
.....
Profit.

Water Line Adjustment-
When using the LowPolyWaterVertexNoise script in conjunction with the FacettedWaterShader,
allow for a bit of room for the water to grow vertically. On start the script will
adjust the y value of the surface you've applied it to and will cause the waterline
to appear higher than the source plane.

Getting the best Low Poly Facetting results-
In order to get results similar to the one included in the test scene, you will need
a 3D mesh that has had the smoothing groups removed and has been triangulated prior
to exporting/importing. A small amount of noise on the surface of the mesh before
exporting can also help increase the effectiveness of the material.

Glossiness blowout-
The glossiness of the surface is dependent on the cubemap that you use, if a less shiny
surface is desired apply an alternative cubemap to the one included.
