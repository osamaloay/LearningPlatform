#Learning Personalized Platform 
A platform intended to enhance the experience of learners to engage in related content and make content delivery specific to learner .the project is under development 

#EERD Schema
$data = file_get_contents('erd\ mile1.xgml');

$loader = new Graphp\GraphML\Loader();
$graph = $loader->loadContents($data);

foreach ($graph->getVertices() as $vertex) {
    foreach ($vertex->getVerticesEdgeTo() as other) {
        echo $vertex->getId() . ' connected with ' . $other->getId() . PHP_EOL;
    }
}
#Future Features 
integration of google meets api to allow the instructors to setup live sessions for their students and make the disscussion rooms live . another feature is suggestion of youtube tutorials for specific course 
