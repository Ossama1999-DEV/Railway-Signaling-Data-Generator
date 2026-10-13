#include <iostream>
#include <tinyxml2.h>
#include <string>
#include <vector>

struct RailwaySignal
{
    std::string signalId;
    std::string type;
    std::string trackId;
    double position;
    std::string direction;
    std::string state;
};

int main(int argc, char* argv[])
{
    if (argc < 2)
    {
        std::cerr << "Usage: RailwaySignalConsumer <xml-file>"
                  << std::endl;
        return 1;
    }

    const char* xmlPath = argv[1];

    tinyxml2::XMLDocument document;

    tinyxml2::XMLError result = document.LoadFile(xmlPath);

    if (result != tinyxml2::XML_SUCCESS)
    {
        std::cerr << "ERROR: Cannot load XML file: "
                  << xmlPath << std::endl;
        return 1;
    }

    std::cout << "XML file loaded successfully!" << std::endl;

    tinyxml2::XMLElement* root =
        document.FirstChildElement("RailwaySignals");

    if (root == nullptr)
    {
        std::cerr << "ERROR: Missing RailwaySignals root"
                  << std::endl;
        return 1;
    }

    // Create vector before the loop
    std::vector<RailwaySignal> signals;

    for (
        tinyxml2::XMLElement* signal =
            root->FirstChildElement("Signal");

        signal != nullptr;

        signal = signal->NextSiblingElement("Signal")
    )
    {
        // 1. Read SignalId
        tinyxml2::XMLElement* signalId =
            signal->FirstChildElement("SignalId");

        if (signalId == nullptr || signalId->GetText() == nullptr)
        {
            std::cerr << "ERROR: Missing SignalId" << std::endl;
            return 1;
        }

        // 2. Read Position
        double position = 0.0;

        tinyxml2::XMLElement* positionElement =
            signal->FirstChildElement("Position");

        if (positionElement == nullptr ||
            positionElement->QueryDoubleText(&position) != tinyxml2::XML_SUCCESS)
        {
            std::cerr << "ERROR: Invalid Position for signal "
                      << signalId->GetText()
                      << std::endl;
            return 1;
        }

        // 3. Create RailwaySignal object
        RailwaySignal railwaySignal;

        railwaySignal.signalId = signalId->GetText();
        railwaySignal.position = position;

        // 4. Add object to vector
        signals.push_back(railwaySignal);

        // 5. Display signal information
        std::cout << "--------------------------" << std::endl;
        std::cout << "Signal ID : "
                  << railwaySignal.signalId << std::endl;

        std::cout << "Position  : "
                  << railwaySignal.position << std::endl;
    }

    std::cout << "--------------------------" << std::endl;
    std::cout << "Total signals: "
              << signals.size()
              << std::endl;

    return 0;
}